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
            //BasketView basket = GetBasket();
            var service = GetService();
            BasketView basket = new BasketView
            {
                BasketID = GetBasketID(),
                BasketLines = service.GetBasketItems(GetBasketID()),
                TotalCost = service.GetTotalCost(GetBasketID())
            };


            return View(basket);
        }

        public ActionResult Create(BasketItemCreate item)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToBasket(int id)
        {
            var service = GetService();
            BasketItemCreate basket = new BasketItemCreate
            {
                VideoID = id,
                BasketID = GetBasketID(),
                DateAdded = DateTime.Now
            };
           

            service.AddToBasket(basket);

            return RedirectToAction("Index");
        }


        
        public ActionResult PlaceOrder()
        {
            var service = GetService();
            BasketView basket = new BasketView
            {
                BasketID = GetBasketID(),
                BasketLines = service.GetBasketItems(GetBasketID()),
                TotalCost = service.GetTotalCost(GetBasketID())
            };


            return View(basket);
        }


        [HttpGet]
        [Route("RemoveBasketLine/{id}")]
        public ActionResult RemoveBasketLine(int id)
        {
            var service = GetService();
            service.RemoveBasketItem(GetBasketID(), id);
            return RedirectToAction("Index");
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


        private BasketView GetBasket()
        {
            BasketView basket = (BasketView)Session["basket"];
            if (basket is null)
            {
                var service = GetService();
                basket = new BasketView
                {
                    BasketID = GetBasketID(),
                    BasketLines = service.GetBasketItems(GetBasketID()),
                    TotalCost = service.GetTotalCost(GetBasketID())
                };
               
            }

            Session["basket"] = basket;
            return basket;
        }


        private BasketService GetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BasketService(userId);
            return service;
        }



    }
}