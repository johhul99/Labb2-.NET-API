using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Labb2_.NET_API.Models
{
    public class Products
    {

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }  
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }  
        public int Quantity { get; set; }
    }
}
