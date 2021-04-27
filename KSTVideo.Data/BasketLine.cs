using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTVideo.Data
{
    public class BasketLine
    {
        public int ID { get; set; }
        public int VideoID { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual Video Video { get; set; }

    }
}
