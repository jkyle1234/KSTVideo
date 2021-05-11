using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTVideo.Data
{
    public class EmailClient
    {
        public int ID { get; set; }
        public string SMTPAddress { get; set; }
        public string FromAddress { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
    }
}
