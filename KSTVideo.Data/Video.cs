using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTVideo.Data
{
    public class Video
    {
        public int ID { get; set; }
        [Display(Name = "Video Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string UPCcode { get; set; }
        public decimal RentalPrice { get; set; }
        public int Quantity { get; set; }
        public int GenreID { get; set; }
        public virtual Genre Genre { get; set; }//navigation property
        public int ImageID { get; set; }
        public string ImageName { get; set; }
       
    }
}
