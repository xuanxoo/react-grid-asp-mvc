using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Accudelta.Service;
using Accudelta.Web.Models;

namespace Accudelta.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IFundService fundService;
        public HomeController(IFundService fundService)
        {
            this.fundService = fundService;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        #region Api

        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult GetFunds(int offset, int limit, string query)
        {
            var data = fundService.GetFunds(offset, limit, query);
            if (data == null)
                return new HttpNotFoundResult();
                   
            var fundsVM = Mapper.Map<IEnumerable<Model.V_FundWithLastValue>, IEnumerable<FundModel>>(data);
            var counter = fundService.CountFunds(query);
            var fundTableViewModel = new FundTableModel()
            {
                TotalCount = counter,
                Funds = fundsVM
            };

            return Json(fundTableViewModel);
        }


        #endregion
    }
}
