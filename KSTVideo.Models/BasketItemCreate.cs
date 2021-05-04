using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTVideo.Models
{
    public class BasketItemCreate
    {
        public int ID { get; set; }
        public int VideoID { get; set; }
        public int Quantity { get; set; }
        public string BasketID { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
