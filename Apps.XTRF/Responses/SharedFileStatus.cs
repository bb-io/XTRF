using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Responses
{
    public class SharedFileStatus
    {
        public string FileId { get; set; }
        public bool Successful { get; set; }
        public string? Message { get; set; }

    }
}
