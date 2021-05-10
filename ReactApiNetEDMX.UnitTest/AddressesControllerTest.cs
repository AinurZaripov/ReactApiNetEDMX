using Microsoft.AspNetCore.Mvc;
using Moq;
using ReactApiNetEDMX.Store.Database;
using ReactApiNetEDMX.Store.Interfaces;
using ReactCoreApiApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Xunit;

namespace ReactApiNetEDMX.UnitTest
{
    public class AddressesControllerTest
    {
        private List<Addresses> GetTestAdresses()
        {
            var addresses = new List<Addresses>
            {
                new Addresses{ Id = 1, Name = "1" },
                new Addresses{ Id = 2, Name = "2" }
            };
            return addresses;
        }
        [Fact]
        public void IndexReturnsAViewResultWithAListOfAddresses()
        {
            // Arrange
            var mock = new Mock<IGenericRepository<Addresses>>();
            mock.Setup(repo => repo.Get()).Returns(GetTestAdresses());
            var controller = new AddressesController(mock.Object);

            // Act
            var result = controller.Get();

            // Assert
            //var viewResult = Assert.IsType<IList<Addresses>>(result);
            var model = Assert.IsAssignableFrom<IList<Addresses>>(result);
            Assert.Equal(GetTestAdresses().Count, model.Count());
        }

        [Fact]
        public void AddUserReturnsARedirectAndAddsUser()
        {
            // Arrange
            var mock = new Mock<IGenericRepository<Addresses>>();
            var controller = new AddressesController(mock.Object);
            var newAdresses = GetTestAdresses().FirstOrDefault();

            // Act
            var result = controller.Post(newAdresses);

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            mock.Verify(r => r.Create(newAdresses));
        }


        //[Fact]
        //public void AddUserReturnsViewResultWithUserModel()
        //{
        //    // Arrange
        //    var mock = new Mock<IGenericRepository<Addresses>>();
        //    var controller = new AddressesController(mock.Object);
        //    controller.ModelState.AddModelError("Name", "Required");
        //    User newUser = new User();

        //    // Act
        //    var result = controller.AddUser(newUser);

        //    // Assert
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    Assert.Equal(newUser, viewResult?.Model);
        //}
    }
}
