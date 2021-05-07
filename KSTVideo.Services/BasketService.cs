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



       

        public void AddToBasket(BasketItemCreate item)
        {
            var ctx = new ApplicationDbContext();
            
                var entity = GetBasketLine(item.BasketID,item.VideoID);

                if (entity == null)
                {
                    entity =
                     new BasketLine()
                     {
                         VideoID = item.VideoID,
                         DateAdded = DateTime.Now,
                         BasketID = item.BasketID,
                       

                     };
                 ctx.BasketLines.Add(entity);
                }
            

            ctx.SaveChanges();
           
               
            
        }


        public void RemoveBasketItem(string basketid, int videoid)
        {
            var ctx = new ApplicationDbContext();
            var basketItem = ctx.BasketLines.FirstOrDefault(b => b.BasketID == basketid && b.Video.ID == videoid);
            if (basketItem != null)
            {
              
                ctx.BasketLines.Remove(basketItem);
                ctx.SaveChanges();
                
            }
        }



        public void EmptyBasket(string basketid)
        {

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
                    total = ctx.BasketLines.Where(b => b.BasketID == basketid).Sum(b => b.Video.RentalPrice);
                }

            }
            return total;
        }


        

    }
}
