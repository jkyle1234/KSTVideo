using KSTVideo.Data;
using KSTVideo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;


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



        public bool SendEmail(BasketView basket)
        {
            var ctx = new ApplicationDbContext();
            var client = ctx.EmailClients.Find(1);
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(client.FromAddress);
                message.To.Add(new MailAddress(client.FromAddress));
                message.Subject = "Order for " + basket.BasketID;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = CreateInvoice(basket);
                smtp.Port = client.Port;
                smtp.Host = client.SMTPAddress; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(client.FromAddress, client.Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
            return true;
        }

        private string CreateInvoice(BasketView basket)
        {
            StringBuilder sb = new StringBuilder();
            foreach(BasketLine b in basket.BasketLines)
            {
                sb.Append(b.Video.Name).Append("\t\t").Append(b.Video.RentalPrice);
                sb.Append("\n");
            }
            return sb.ToString();
        }



        public void EmptyBasket(string basketid)
        {
            var ctx = new ApplicationDbContext();
            ctx.BasketLines.RemoveRange(ctx.BasketLines.Where(b => b.BasketID == basketid));
            ctx.SaveChanges();
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
