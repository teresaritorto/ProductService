﻿using ProductService.Data;
using ProductService.Models;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace ProductService.Controllers
{
    /// <summary>
    /// Product API
    /// </summary>    
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private IProductRepository repository;

        /// <summary>
        /// Product Service
        /// </summary>
        public ProductsController()
        {
            repository = new ProductRepository();
        }

        /// <summary>
        /// GET All products
        /// </summary>
        /// <returns>List of products</returns>        
        [Route("api/products"), HttpGet]
        public IHttpActionResult GetAllProducts()
        {
            try
            {
                return Ok(repository.GetAll());
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// GET product by Id             
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns></returns>
        [Route("api/products/{id}"), HttpGet]
        public IHttpActionResult GetProduct(string id)
        {
            try
            {
                var product = repository.Get(id);

                if (product == null)
                    return NotFound();

                return Ok(product);
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// GET products by filter
        /// </summary>
        /// <param name="query">query</param>
        /// <returns></returns>
        [HttpGet]        
        public IHttpActionResult GetByFilter([FromUri]FilterProductQuery query)
        {
            try
            {
                var product = repository.GetByFilter(query.Model, query.Description, query.Brand);

                if (product.Count() == 0)
                    return NotFound();

                return Ok(product);
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// PUT Update product
        /// </summary>
        /// <param name="id">product id</param>
        /// <param name="value">product</param>
        [Route("api/products/{id}"), HttpPut]
        public IHttpActionResult Put(string id, [FromBody]Product value)
        {
            try
            {
                var product = repository.Get(id);
                if (product != null)
                    return Ok(repository.Update(value));
                else
                    return NotFound();
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// POST create product
        /// </summary>        
        /// <param name="value">product</param>
        [Route("api/products"), HttpPost]
        public IHttpActionResult Post([FromBody]Product value)
        {
            try
            {
                return Ok(repository.Add(value));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// DELETE: removes product by id
        /// </summary>
        /// <param name="id">product id</param>
        [Route("api/products/{id}"), HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                repository.Delete(id);
                return Ok();
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }      
    }
}