using KSTVideo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTVideo.Models
{
    public class BasketView
    {
        
        public string BasketID { get; set; }
        public List<BasketLine> BasketLines { get; set; }
        [Display(Name = "Basket Total:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCost { get; set; }
    }
}
