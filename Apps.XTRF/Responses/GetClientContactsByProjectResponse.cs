using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Responses
{
    public class GetClientContactsByProjectResponse
    {
        public int PrimaryId { get; set; }
        public int[] AdditionalIds { get; set; }
    }
}
