using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductService.Data;
using ProductService.Models;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ProductService.Tests
{
    [TestClass]
    public class ProductRepositoryTest
    {
        private Mock<IRedisClient> mockRedis;
        private Mock<IRedisTypedClient<Product>> mockProducts;
        private Product _product;

        public ProductRepositoryTest()
        {        
        }

        [TestInitialize]
        public void Initialize()
        {
            mockRedis = new Mock<IRedisClient>();
            mockProducts = new Mock<IRedisTypedClient<Product>>();
            mockRedis.Setup(x => x.As<Product>()).Returns(mockProducts.Object);
            mockRedis.Setup(x => x.Ping()).Returns(true);

            _product = new Product
            {
                Id = "1",
                Brand = "brand a",
                Model = "model a",
                Description = "description a"
            };
        }
        

        [TestMethod]
        public void AddProduct()
        {
            //Setup
            mockProducts.Setup(x => x.GetNextSequence()).Returns(1);
            mockProducts.Setup(x => x.Store(_product)).Returns(_product);

            var repository = new ProductRepository(mockRedis.Object);

            //Act
            var newProduct = repository.Add(_product);

            //Assert
            Assert.IsNotNull(newProduct);
            Assert.IsTrue(newProduct.Id == _product.Id);
            Assert.IsTrue(newProduct.Brand == _product.Brand);
            Assert.IsTrue(newProduct.Model == _product.Model);
            Assert.IsTrue(newProduct.Description == _product.Description);
        }

        [TestMethod]
        public void GetProduct()
        {
            //Setup 
            mockProducts.Setup(x => x.GetById(_product.Id)).Returns(_product);
            var repository = new ProductRepository(mockRedis.Object);

            //Act
            var product = repository.Get(_product.Id);

            //Assert
            Assert.IsNotNull(product);
            Assert.IsTrue(product.Id == _product.Id);
            Assert.IsTrue(product.Brand == _product.Brand);
            Assert.IsTrue(product.Model == _product.Model);
            Assert.IsTrue(product.Description == _product.Description);
        }

        [TestMethod]
        public void DeleteProduct()
        {
            //Setup 
            mockProducts.Setup(x => x.DeleteById(_product.Id));
            var repository = new ProductRepository(mockRedis.Object);

            //Act
            repository.Delete(_product.Id);

            //Verify            
            mockProducts.Verify(mock => mock.DeleteById(_product.Id), Times.Once());
        }

        [TestMethod]
        public void UpdateProduct()
        {
            var product = new Product
            {
                Brand = "brand b",
                Description = "description b",
                Model = "model b"
            };


            //Setup
            mockProducts.Setup(x => x.Store(product)).Returns(product);
            var repository = new ProductRepository(mockRedis.Object);

            //Act
            var newProduct = repository.Update(product);

            //Assert
            Assert.IsNotNull(newProduct);

            //Verify            
            mockProducts.Verify(mock => mock.Store(newProduct), Times.Once());

        }

        [TestMethod]
        public void GetAllProducts()
        {
            //Setup
            mockProducts.Setup(x => x.GetAll()).Returns(new List<Product>{ _product, _product, _product });
            var repository = new ProductRepository(mockRedis.Object);

            //Act
            IEnumerable<Product> products = repository.GetAll();

            //Assert
            Assert.IsTrue(products.Count() == 3);
        }

    }
}
