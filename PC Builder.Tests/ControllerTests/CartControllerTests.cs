using Microsoft.VisualStudio.TestTools.UnitTesting;
using PC_Builder.Controllers;
using Microsoft.AspNetCore.Mvc;
using PC_Builder.Models;
using PC_Builder.ViewModels;
using Newtonsoft.Json;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace PC_Builder.Tests.ControllerTests
{
    [TestClass]
    public class CartControllerTests
    {
        private DbContextOptions<ApplicationContext> options;

        [TestMethod]
        public void Can_Use_Repository()
        {
            // arrange
            using var dbContext = new ApplicationContext(options);
            dbContext.Products.Add(new ProductInCart {
                //new Product {
                //    Name = "Product1",
                //    Category = "CPU",
                //    Price = 234,
                //    ProductCounter = 1,
                //    Subtotal = 234,
                //    ProductId = Guid.Parse("9546482E-887A-4CAB-A403-AD9C326FFDA5"),
                //    UserLogin = "user" },

                //new Product {
                //    Name = "Product2",
                //    Category = "CPU",
                //    Price = 234,
                //    ProductCounter = 1,
                //    Subtotal = 234,
                //    ProductId = Guid.Parse("9546482E-887A-4CAB-A403-AD9C326FFDA5"),
                //    UserLogin = "user" }
            });
            dbContext.SaveChanges();


            //CartController controller = new CartController(mock.Object);

            // act

            // assert
            
        }
    }
}