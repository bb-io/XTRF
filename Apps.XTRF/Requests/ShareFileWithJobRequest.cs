using Apps.XTRF.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Requests
{
    public class ShareFileWithJobRequest
    {
        public string JobId { get; set; }
        public string FileId { get; set; }
    }
}
