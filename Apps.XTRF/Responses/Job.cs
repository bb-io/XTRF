using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Responses
{
    public class Job
    {
        public string Id { get; set; }
        public string IdNumber { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int StepNumber { get; set; }
        public int? VendorId { get; set; }
        public int? VendorPriceProfileId { get; set; }
        public StepType StepType { get; set; }

    }

    public class StepType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int JobTypeId { get; set; }
    }

}
