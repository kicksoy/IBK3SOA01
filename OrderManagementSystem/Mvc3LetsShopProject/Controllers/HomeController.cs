using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataClassLibrary;
using DataClassLibrary.Models;

namespace Mvc3LetsShopProject.Controllers
{
    /// <summary>
    /// The home controller is used mainly for displaying the home page.
    /// </summary>

    public class HomeController : Controller
    {
        
        /// <summary>
        /// GET: Home/Index
        /// </summary>
        /// <returns></returns>
        
        public ActionResult Index()
        {
            

            List<Product> productcacheData = LetsShopImplementation.GetProducts();
            
            return View(productcacheData);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: Home/About
        /// </summary>
        /// <returns></returns>
        
        public ActionResult About()
        {
            return View();
        }

        //______________________________________________________________________________________

    }
}
