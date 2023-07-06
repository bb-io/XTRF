namespace Apps.XTRF.Responses.Models
{
    public class Customer : SimpleCustomer
    {
        public string idNumber { get; set; }
        public string FullName { get; set; }
        public string Notes { get; set; }

        //public Address BillingAddress { get; set; }
        //public Address CorrespondenceAddress { get; set; }
        //public Contact Contact { get; set; }
        public int BranchId { get; set; }
        public string Status { get; set; }
        public string ContractNumber { get; set; }
        public string SalesNotes { get; set; }
        public int ClientNumberOfProjects { get; set; }
        public int ClientNumberOfQuotes { get; set; }

    }
}
