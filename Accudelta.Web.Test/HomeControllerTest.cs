using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Moq;
using Accudelta.Data.Interface;
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
        IRepository<V_FundWithLastValue> fundRepository;
        private IFundService fundService;

        [TestInitialize]
        public void Initialize()
        {
            //Mock service creation
            Mock<IRepository<V_FundWithLastValue>> mock = new Mock<IRepository<V_FundWithLastValue>>();
            mock.Setup(m => m.GetQuery()).Returns(new List<V_FundWithLastValue> {
                new V_FundWithLastValue { Id = 1, Name = "Fund 1", Description = "Fund description 1", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 2, Name = "Fund 2", Description = "Fund description 2", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 3, Name = "Fund 3", Description = "Fund description 3", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 4, Name = "Fund 4", Description = "Fund description 4", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 5, Name = "Fund 5", Description = "Fund description 5", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 6, Name = "Fund 6", Description = "Fund description 6", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 7, Name = "Fund 7", Description = "Fund description 7", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 8, Name = "Fund 8", Description = "Fund description 8", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 9, Name = "Fund 9", Description = "Fund description 9", Value = 1.33m, Date = new DateTime() },
                new V_FundWithLastValue { Id = 10, Name = "Fund 10", Description = "Fund description 10", Value = 1.33m, Date = new DateTime() }
            }.AsQueryable());                        
            fundRepository = mock.Object;

            this.fundService = new FundService(fundRepository);

            //initialize automapper
            AutoMapperConfig.Configure();
        }

        [TestMethod]
        public void is_returns_first_ten_funds()
        {
            
            //Create the controller instance
            HomeController homeController = new HomeController(fundService);

            //Act
            var viewResult = (JsonResult)homeController.GetFunds(0, 10, "");
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
            var viewResult = (JsonResult)homeController.GetFunds(0, 10, "fund 1");
            var fundTableModel = (FundTableModel)viewResult.Data;

            //Assert
            Assert.AreEqual<int>(2, fundTableModel.TotalCount);
        }

        [TestMethod]
        public void is_returns_search_funds()
        {
            //Create the controller instance
            HomeController homeController = new HomeController(fundService);

            //Act
            var viewResult = (JsonResult)homeController.GetFunds(0, 10, "fund 1");
            var fundTableModel = (FundTableModel)viewResult.Data;

            //Assert
            Assert.AreEqual<int>(2, fundTableModel.Funds.Count());
        }
    }
}

