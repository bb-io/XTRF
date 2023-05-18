using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Responses.Models
{
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string ErrorMessage { get; set; }
        public string? DetailedMessage { get; set; }
    }
}
