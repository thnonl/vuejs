using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using Vue2Spa.Models;

namespace Vue2Spa.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> Gets();

        long GetTotal();
 
        Product GetProduct(ObjectId id);
 
        Product Create(Product p);
 
        void Update(ObjectId id, Product p);
        void Remove(ObjectId id);
    }
}
