using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Smart.Models.Dtos
{
    public class ContactDto
    {
        public List<string> Phones { get; set; } = new();

        public string Sms { get; set; } = string.Empty;

        public string Fax { get; set; } = string.Empty;

        public EmailsDto Emails { get; set; } = new();

        public List<string> Websites { get; set; } = new();
    }

}
