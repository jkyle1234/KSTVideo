using KSTVideo.Models;
using KSTVideo.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KSTVideo.Controllers
{
    public class BasketController : Controller
    {
        private string sessionkey = "";

        // GET: Basket
        public ActionResult Index()
        {
            var service = GetService();
            string basketId = GetBasketID();
            BasketView basket = new BasketView
            {
                BasketID = basketId,
                BasketLines = service.GetBasketItems(basketId),
                TotalCost = service.GetTotalCost(basketId)
            };
            return View(basket);
        }

        public ActionResult Create(BasketItemCreate item)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToBasket(int id, int quantity)
        {
            var service = GetService();
            BasketItemCreate basket = new BasketItemCreate
            {
                VideoID = id,
                Quantity = quantity,
                BasketID = GetBasketID(),
                DateAdded = DateTime.Now
            };

            service.AddToBasket(basket);
           
            return RedirectToAction("Index");
        }


        public ActionResult UpdateBasket(BasketView basket)
        {
            var service = GetService();
            bool updated = service.UpdateBasket(basket);

            return null;
        }


        private string GetBasketID()
        {
            
            if (HttpContext.Session[sessionkey] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
                {
                    HttpContext.Session[sessionkey] =
                    HttpContext.User.Identity.Name;
                }
                else
                {
                    Guid tempBasketID = Guid.NewGuid();
                    HttpContext.Session[sessionkey] = tempBasketID.ToString();
                }
            }
            return HttpContext.Session[sessionkey].ToString();
        }


        private BasketService GetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BasketService(userId);
            return service;
        }



    }
}