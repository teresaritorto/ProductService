using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ProductService.Controllers
{
    public class ProductsController : ApiController
    {
        //TODO - In-memory repo for products - use DI to pass into controller
        
        //GET products
        public IEnumerable<Product> GetAllProducts()
        {
            throw new NotImplementedException();
          
        }

        //GET products/{id}
        public IHttpActionResult GetProduct(string id)
        {
            throw new NotImplementedException();          
        }
    }
}
