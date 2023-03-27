using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Responses
{
    public class Project
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public bool IsClassicProject { get; set; }
        public string QuoteIdNumber { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string BudgetCode { get; set; }
        public int ClientId { get; set; }
        public int ServiceId { get; set; }
        public string Origin { get; set; }
        public int ClientDeadline { get; set; }
        public string ClientReferenceNumber { get; set; }
        public string ClientNotes { get; set; }
        public string InternalNotes { get; set; }
        public string ProjectIdNumber { get; set; }
        public string InstructionsForAllJobs { get; set; }
        public int OrderedOn { get; set; }
    }
}
