using ProductService.Data;
using ProductService.Models;
using System.Linq;
using System.Web.Http;

namespace ProductService.Controllers
{
    /// <summary>
    /// Product API
    /// </summary>    
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
        [Route("products"), HttpGet]
        public IHttpActionResult GetAllProducts()
        {
            return Ok(repository.GetAll());
        }

        /// <summary>
        /// GET product by Id             
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns></returns>
        [Route("products/{id}"), HttpGet]
        public IHttpActionResult GetProduct(string id)
        {
            var product = repository.Get(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        /// <summary>
        /// GET products by filter
        /// </summary>
        /// <param name="q">query</param>
        /// <returns></returns>        
        [HttpGet]
        public IHttpActionResult GetByFilter([FromUri]FilterProductQuery q)
        {
            var product = repository.GetByFilter(q.Model, q.Description, q.Brand);

            if (product.Count() == 0)
                return NotFound();

            return Ok(product);
        }

        /// <summary>
        /// PUT Update product
        /// </summary>
        /// <param name="id">product id</param>
        /// <param name="value">product</param>
        [Route("products/{id}"), HttpPut]
        public IHttpActionResult Put(string id, [FromBody]Product value)
        {
            var product = repository.Get(id);
            if (product != null)
                return Ok(repository.Update(value));
            else
                return NotFound();
        }

        /// <summary>
        /// POST create product
        /// </summary>        
        /// <param name="value">product</param>
        [Route("products"), HttpPost]
        public IHttpActionResult Post([FromBody]Product value)
        {
            return Ok(repository.Add(value));
        }

        /// <summary>
        /// DELETE: removes product by id
        /// </summary>
        /// <param name="id">product id</param>
        [Authorize(Roles = UserRepository.Admin)]
        [Route("products/{id}"), HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            repository.Delete(id);
            return Ok();
        }
    }
}
