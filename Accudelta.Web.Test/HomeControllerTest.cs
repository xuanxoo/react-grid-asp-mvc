using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Moq;
using Accudelta.Service;
using Accudelta.Model;
using Accudelta.Web.Controllers;
using Accudelta.Web.Models;
using System.Web.Mvc;

namespace Accudelta.Web.Test
{
    [TestClass]
    public class HomeControllerTest
    {

        private IFundService fundService;

        [TestInitialize]
        public void Initialize()
        {
            //Mock service creation
            Mock<IFundService> mock = new Mock<IFundService>();
            mock.Setup(m => m.GetFunds(0, 10)).Returns(new List<V_FundWithLastValue> {
                new V_FundWithLastValue { Id = 1, Name = "Found 1", Description = "Found description 1", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 2, Name = "Found 2", Description = "Found description 2", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 3, Name = "Found 3", Description = "Found description 3", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 4, Name = "Found 4", Description = "Found description 4", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 5, Name = "Found 5", Description = "Found description 5", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 6, Name = "Found 6", Description = "Found description 6", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 7, Name = "Found 7", Description = "Found description 7", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 8, Name = "Found 8", Description = "Found description 8", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 9, Name = "Found 9", Description = "Found description 9", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 10, Name = "Found 10", Description = "Found description 10", Value = 1.33m, Date = new DateTime() }
            }.ToList());

            mock.Setup(m => m.CountFunds()).Returns(1000000);
            fundService = mock.Object;

            //initialize automapper
            AutoMapperConfig.Configure();
        }

        [TestMethod]
        public void is_returns_first_ten_funds()
        {
            //Create the controller instance
            HomeController homeController = new HomeController(fundService);

            //Act
            var viewResult = (JsonResult)homeController.GetFunds(0, 10);
            var fundTableModel = (FundTableModel)viewResult.Data;

            //Assert
            Assert.AreEqual<int>(10, fundTableModel.Funds.Count());
        }

        [TestMethod]
        public void is_returns_totalfunds_count()
        {
            //Create the controller instance
            HomeController homeController = new HomeController(fundService);

            //Act
            var viewResult = (JsonResult)homeController.GetFunds(0, 10);
            var fundTableModel = (FundTableModel)viewResult.Data;

            //Assert
            Assert.AreEqual<int>(1000000, fundTableModel.TotalCount);
        }
    }
}

