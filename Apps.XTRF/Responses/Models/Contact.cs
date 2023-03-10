using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Responses.Models
{
    public class Contact
    {
        public string Sms { get; set; }
        public string Fax { get; set; }
        public Emails Emails { get; set; }
    }
}
