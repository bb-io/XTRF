using Apps.XTRF.Utils.Converters;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.XTRF.Models.Responses.Entities;

public class Customer : SimpleCustomer
{
    [Display("ID number")] public string IdNumber { get; set; }
    [Display("Full name")] public string FullName { get; set; }
    public string Notes { get; set; }

    //public Address BillingAddress { get; set; }
    //public Address CorrespondenceAddress { get; set; }
    //public Contact Contact { get; set; }
    [Display("Branch ID")]
    [JsonConverter(typeof(IntToStringConverter))]
    public string BranchId { get; set; }

    public string Status { get; set; }
    [Display("Contract number")] public string ContractNumber { get; set; }
    [Display("Sales notes")] public string SalesNotes { get; set; }
    [Display("Client number of projects")] public int ClientNumberOfProjects { get; set; }
    [Display("Client number of quotes")] public int ClientNumberOfQuotes { get; set; }
}