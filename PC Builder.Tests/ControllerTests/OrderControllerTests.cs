using Microsoft.VisualStudio.TestTools.UnitTesting;
using PC_Builder.Controllers;
using Microsoft.AspNetCore.Mvc;
using PC_Builder.Models;
using PC_Builder.ViewModels;
using Newtonsoft.Json;

namespace PC_Builder.Tests.ControllerTests
{
    [TestClass]
    public class OrderControllerTests
    {
        [TestMethod]
        public void Order(ApplicationContext context)
        {
            // arrange

            OrderController controller = new OrderController(context);

            // act

            // assert
            
        }
    }
}