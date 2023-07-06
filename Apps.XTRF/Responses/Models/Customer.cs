using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Responses.Models
{
    public class Customer : SimpleCustomer
    {
        [Display("Id number")] public string idNumber { get; set; }
        [Display("Full name")] public string FullName { get; set; }
        public string Notes { get; set; }

        //public Address BillingAddress { get; set; }
        //public Address CorrespondenceAddress { get; set; }
        //public Contact Contact { get; set; }
        [Display("Branch id")] public int BranchId { get; set; }
        public string Status { get; set; }
        [Display("Contract number")] public string ContractNumber { get; set; }
        [Display("Sales notes")] public string SalesNotes { get; set; }
        [Display("Client number of projects")] public int ClientNumberOfProjects { get; set; }
        [Display("Client number of quotes")] public int ClientNumberOfQuotes { get; set; }

    }
}
