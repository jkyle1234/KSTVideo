using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTVideo.Models
{
    public class VideoCreate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UPCcode { get; set; }
        public decimal RentalPrice { get; set; }
        public int GenreID { get; set; }
        public string ImageName { get; set; }
    }
}
