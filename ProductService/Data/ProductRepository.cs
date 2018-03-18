using System.Collections.Generic;
using ProductService.Models;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using ProductService.Helpers;
using System.Linq;

namespace ProductService.Data
{
    /// <summary>
    /// Product Repository
    /// </summary>
    public class ProductRepository : IProductRepository, IDisposable
    {
        private readonly IRedisClient client;
        private IRedisTypedClient<Product> redisProducts;

        /// <summary>
        /// Product Repository
        /// </summary>
        public ProductRepository()
        {            
            client = new RedisClient(ConfigSettings.RedisServer, ConfigSettings.RedisPort);
            redisProducts = client.As<Product>();                
        }

     

        /// <summary>
        /// Product Repository
        /// </summary>
        /// <param name="client"></param>
        public ProductRepository(IRedisClient client)
        {
            this.client = client;
            redisProducts = this.client.As<Product>();           
        }
        
        /// <summary>
        /// Get Product
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Product</returns>
        public Product Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;
            
            return redisProducts.GetById(id);
        }

        /// <summary>
        /// Get Product by Filter - search within by model, description & brand to return products matching
        /// </summary>
        /// <param name="model"></param>
        /// <param name="description"></param>
        /// <param name="brand"></param>
        /// <returns>Collection of products</returns>
        public IEnumerable<Product> GetByFilter(string model, string description, string brand)
        {
            var filter = string.IsNullOrEmpty(description) ? new List<Product>() : redisProducts.GetAll().Where(p => (p.Description.ToLower().Contains(description.ToLower()))).ToList();
            var brandFilter = string.IsNullOrEmpty(brand) ? new List<Product>() : redisProducts.GetAll().Where(p => (p.Brand.ToLower() == brand.ToLower())).ToList();
            var modelFilter = string.IsNullOrEmpty(model) ? new List<Product>() : redisProducts.GetAll().Where(p => (p.Model.ToLower() == model.ToLower())).ToList();
            
            //merge and get distinct
            var allProducts = filter.Concat(modelFilter).Concat(brandFilter);
            return allProducts.GroupBy(x => x.Id).Select(x => x.First());            
        }

        /// <summary>
        /// Delete product by id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
                redisProducts.DeleteById(id);
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>Collection of products</returns>
        public IEnumerable<Product> GetAll()
        {
            return redisProducts.GetAll();
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product">Product update</param>
        /// <returns>Updated product</returns>
        public Product Update(Product product)
        {
            if (product == null)
                throw new NullReferenceException("Unable to update, product is required.");
            
            return redisProducts.Store(product);
        }

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="product">Product added</param>
        /// <returns>Product added</returns>
        public Product Add(Product product)
        {
            if (product == null)
                throw new NullReferenceException("Unable to add, product is required.");

            product.Id = redisProducts.GetNextSequence().ToString();
            return redisProducts.Store(product);
        }        

        
        /// <summary>
        /// Dispose of client
        /// </summary>
        public void Dispose()
        {
            client.Dispose();
        }
    }
}