using KSTVideo.Data;
using KSTVideo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KSTVideo.Services
{
    public class BasketService
    {

        private string BasketID { get; set; }
        private readonly Guid _userid;
       

        public BasketService(Guid userid)
        {
            _userid = userid;
        }



        public bool UpdateBasket(BasketView basket)
        {
            var ctx = new ApplicationDbContext();
            foreach(BasketLine b in basket.BasketLines)
            {
                var item = GetBasketLine(b.BasketID, b.VideoID);
                if(item != null)
                {
                    if (item.Quantity == 0)
                    {
                        RemoveBasketItem(item.Video.ID);
                    }
                    else
                    {
                        item.Quantity = b.Quantity;
                    }
                }
            }
            return ctx.SaveChanges() >= 0;
        }


        public bool AddToBasket(BasketItemCreate item)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = GetBasketLine(item.VideoID);

                if (entity == null)
                {
                    entity =
                     new BasketLine()
                     {
                         VideoID = item.VideoID,
                         DateAdded = DateTime.Now,
                         BasketID = item.BasketID,
                         Quantity = item.Quantity

                     };
                 ctx.BasketLines.Add(entity);
                }
                else
                {
                    entity.Quantity += item.Quantity;
                }
               
               return ctx.SaveChanges() == 1;
            }
        }


        public void RemoveBasketItem(int videoid)
        {
            var basketItem = GetBasketLine(videoid);
            if (basketItem != null)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    ctx.BasketLines.Remove(basketItem);
                    ctx.SaveChanges();
                }
            }
        }



        public void EmptyBasket(string basketid)
        {

        }


        private BasketLine GetBasketLine(int id)
        {
            
                var ctx = new ApplicationDbContext();
                var bl = ctx.BasketLines.FirstOrDefault(b => b.VideoID == id);
                return bl;
        }

        private BasketLine GetBasketLine(string basketid,int videoid)
        {
            var ctx = new ApplicationDbContext();
            return ctx.BasketLines.FirstOrDefault(b => b.BasketID == basketid && b.Video.ID == videoid);
        }


        public List<BasketLine> GetBasketItems(string basketid)
        {
            var ctx = new ApplicationDbContext();
            return ctx.BasketLines.Where(b => b.BasketID == basketid).ToList();
            
        }

        public decimal GetTotalCost(string basketid)
        {
            decimal total = decimal.Zero;

            using (var ctx = new ApplicationDbContext())
            {
                if(GetBasketItems(basketid).Count > 0)
                {
                    total = ctx.BasketLines.Where(b => b.BasketID == basketid).Sum(b => b.Video.RentalPrice * b.Quantity);
                }

            }
            return total;
        }


        

    }
}
