
using MongoDB.Driver;
using Labb2_.NET_API.Data;
using Labb2_.NET_API.Models;

namespace Labb2_.NET_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            CRUD db = new CRUD("Products", configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //Create
            app.MapPost("/product", async (Products product) =>
            {
                var productDB = await db.AddProduct("Products", product);
                return Results.Ok(productDB);
            });

            //Update
            app.MapPut("/product/{id}", async (string id, string name, string category, decimal price, int quantity) =>
            {
                var products = await db.UpdateProduct("Products", id, name, category, price, quantity);
                return Results.Ok(products);
            });

            //Read
            app.MapGet("/products", async () =>
            {
                var products = await db.GetAllProducts("Products");
                return Results.Ok(products);
            });

            //ReadById
            app.MapGet("/products/{id}", async (string id) =>
            {
                var product = await db.GetProductById("Products", id);
                return Results.Ok(product);
            });

            //Delete
            app.MapDelete("/product/{id}", async (string id) =>
            {
                var product = await db.DeleteProduct("Products", id);
                return Results.Ok(product);
            });

            

            app.Run();
        }
    }
}
