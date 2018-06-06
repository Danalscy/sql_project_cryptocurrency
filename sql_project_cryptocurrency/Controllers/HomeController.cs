using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sql_project_cryptocurrency.WebService;
using sql_project_cryptocurrency.Models;

namespace sql_project_cryptocurrency.Controllers
{
    public class HomeController : Controller
    {
        private readonly CryptoWebService _CryptoWebService;
        public HomeController()
        {
            _CryptoWebService = new CryptoWebService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Power BI Report";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "KDD";

            return View();
        }
        
        public ActionResult GetPredictionFromWebService()
        {
            var id = Request.Form["ID_crypto"];
            if (!string.IsNullOrEmpty(id))
            {
                //var resultResponse = _CryptoWebService.InvokeRequestResponseService(id).Result;
                var predicted_price = _CryptoWebService.InvokeRequestResponseService(id).Result;
                //Debug.WriteLine(predicted_price);
                ViewBag.Id = id;
                ViewBag.Predicted = predicted_price;
            }

            return View();
        }
        

    }
}