namespace Vue2Spa.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using Vue2Spa.Helper;
    using Vue2Spa.Models;

    public interface IProductRepository
    {
        IQueryable<Product> Gets();

        long GetTotal();
 
        Product GetProduct(ObjectId id);
 
        Product Create(Product p);
 
        void Update(ObjectId id, Product p);
        void Remove(ObjectId id);
    }
    
    public class ProductRepository : IProductRepository
    {
        MongoClient _client;
        IMongoDatabase _db;

        public ProductRepository()
        {
            _client = new MongoClient($"mongodb://{Constants.Server.Username}:{Constants.Server.Password}@{Constants.Server.Url}");
            _db = _client.GetDatabase("pwasimpleapp");
        }

        public IQueryable<Product> Gets()
        {
            return _db.GetCollection<Product>(nameof(Product)).AsQueryable();
        }

        public long GetTotal()
        {
            var now = DateTime.Now;
            var total = _db.GetCollection<Product>(nameof(Product)).CountDocuments(Query<Product>.LT(p => p.CreatedOn, now).ToBsonDocument());
            return total;
        }
 
        public Product GetProduct(ObjectId id)
        {
            return _db.GetCollection<Product>(nameof(Product)).AsQueryable().FirstOrDefault(p => p.Id == id);
        }
 
        public Product Create(Product p)
        {
            _db.GetCollection<Product>(nameof(Product)).InsertOne(p);
            return p;
        }
 
        public void Update(ObjectId id, Product p)
        {
            p.Id = id;
            var res = Query<Product>.EQ(pd => pd.Id, id).ToBsonDocument();
            _db.GetCollection<Product>(nameof(Product)).ReplaceOne(res, p);
        }
        public void Remove(ObjectId id)
        {
            var res = Query<Product>.EQ(e => e.Id, id).ToBsonDocument();
            var operation = _db.GetCollection<Product>(nameof(Product)).DeleteOne(res);
        }
    }
}
