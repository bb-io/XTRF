using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Smart.Models.Dtos
{
    public class EmailsDto
    {
        public string Primary { get; set; } = string.Empty;

        public List<string> Cc { get; set; } = new();

        public List<string> Additional { get; set; } = new();
    }

}
