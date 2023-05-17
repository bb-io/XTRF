using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Responses.Models
{
    public class SimpleProject
    {
        public string Name { get; set; }
        public int ClientId { get; set; }
        public int ServiceId { get; set; }
        public string ExternalId { get; set; }
    }
}
