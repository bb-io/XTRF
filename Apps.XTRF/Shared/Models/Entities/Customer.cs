using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Entities;

public class SimpleCustomer
{
    [Display("Customer")]
    public string Id { get; set; }
    
    public string Name { get; set; }
}

public class Customer : SimpleCustomer
{
    [Display("Full name")] 
    public string FullName { get; set; }

    [Display("ID Number")]
    public string idNumber { get; set; }
    
    public string Notes { get; set; }

    [Display("Billing address")]
    public Address BillingAddress { get; set; }
    
    [Display("Correspondence address")]
    public Address CorrespondenceAddress { get; set; }
    
    public Contact Contact { get; set; }
    
    public IEnumerable<ContactPerson> Persons { get; set; }

    [Display("Branch ID")]
    public string BranchId { get; set; }

    public string Status { get; set; }
    
    [Display("Contract number")] 
    public string ContractNumber { get; set; }
    
    [Display("Sales notes")] 
    public string SalesNotes { get; set; }
    
    [Display("Client number of projects")] 
    public int ClientNumberOfProjects { get; set; }
    
    [Display("Client number of quotes")] 
    public int ClientNumberOfQuotes { get; set; }
}

public class ContactPerson
{
    [Display("Person ID")]
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    [Display("Last name")]
    public string? LastName { get; set; }
    
    public Contact Contact { get; set; }
    
    [Display("Mother tongues")]
    public IEnumerable<string> MotherTonguesIds { get; set; }
}