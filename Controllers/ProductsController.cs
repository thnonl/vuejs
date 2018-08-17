using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Vue2Spa.Models;
using Vue2Spa.Repositories;

namespace Vue2Spa.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _respository;
        public ProductsController (IProductRepository respository)
        {
             this._respository = respository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product prod)
        {
            prod.CreatedOn = DateTime.Now;
            _respository.Create(prod);

            return NoContent();
        }

        [HttpPost("[action]/{productId:length(24)}")]
        public IActionResult Update(string productId, [FromBody] Product prod)
        {
            ObjectId id = new ObjectId(productId);
            _respository.Update(id, prod);
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _respository.Gets();
            return new OkObjectResult(products.ToList());
        }

        [HttpGet("{productId:length(24)}")]
        public IActionResult GetById(string productId)
        {
            var product = _respository.GetProduct(new ObjectId(productId));
            if (product != null) {
                return new OkObjectResult(product);
            }
            return NotFound();
        }

        [HttpDelete("{productId:length(24)}")]
        public IActionResult Delete (string productId)
        {
            _respository.Remove(new ObjectId(productId));
            return NoContent();
        }
    }
}
