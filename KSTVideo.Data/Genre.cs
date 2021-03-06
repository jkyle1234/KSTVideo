using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTVideo.Data
{
    public class Genre
    {
        public int ID { get; set; }
        [Display(Name = "Genre")]
        public string Name { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}
