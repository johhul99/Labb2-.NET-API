using MongoDB.Driver;
using Labb2_.NET_API.Models;
using MongoDB.Driver.Linq;

namespace Labb2_.NET_API.Data
{
    public class CRUD
    {
        private IMongoDatabase db;

        public CRUD(string database, IConfiguration configuration )
        {
            var client = new MongoClient(configuration.GetConnectionString("DefaultConnection"));
            db = client.GetDatabase(database);
        }

        //Create
        public async Task<List<Products>> AddProduct(string table, Products product)
        {
            var collection = db.GetCollection<Products>(table);
            await collection.InsertOneAsync(product);
            return collection.AsQueryable().ToList();
        }

        //Update
        public async Task<List<Products>> UpdateProduct(string table, string id, string name, string category, decimal price, int quantity)
        {
            var collection = db.GetCollection<Products>(table);
            var product = await collection.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (product != null)
            {
                product.Name = name;
                product.Category = category;
                product.Price = price;
                product.Quantity = quantity;

                await collection.ReplaceOneAsync(p => p.Id == id, product);
            }

            return await collection.Find(_ => true).ToListAsync(); 
        }

        //Read
        public async Task<List<Products>> GetAllProducts(string table)
        {
            var collection = db.GetCollection<Products>(table);
            var products = await collection.AsQueryable().ToListAsync();
            return products;
        }

        //ReadById
        public async Task<Products> GetProductById(string table, string id)
        {
            var collection = db.GetCollection<Products>(table);
            var product = await collection.Find(p => p.Id == id).FirstOrDefaultAsync();
            return product;
        }

        //Delete
        public async Task<string> DeleteProduct(string table, string id)
        {
            var collection = db.GetCollection<Products>(table);
            var product = await collection.DeleteOneAsync(p => p.Id == id);
            return "Deleted product";
        }
    }
}
