using Apps.XTRF.Classic.Models.Responses.ClassicTask;
using Apps.XTRF.Shared.Extensions;
using Apps.XTRF.Shared.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Classic.Models.Responses.ClassicQuote;

public class QuoteResponse
{
    public QuoteResponse(Entities.ClassicQuote quote)
    {
        Id = quote.Id;
        IsClassicQuote = quote.IsClassicQuote;
        IdNumber = quote.IdNumber;
        Name = quote.Name;
        CustomerId = quote.CustomerId;
        ContactPersonId = quote.ContactPersonId;
        Status = quote.Status;
        StartDate = quote.Dates.StartDate?.Time?.ConvertFromUnixTime();
        Deadline = quote.Dates.Deadline?.Time?.ConvertFromUnixTime();
        CreatedOn = quote.Dates.CreatedOn?.Time?.ConvertFromUnixTime();
        OfferExpiry = quote.Dates.OfferExpiry?.Time?.ConvertFromUnixTime();
        Instructions = quote.Instructions;
        Finance = quote.Finance;
        CategoriesIds = quote.CategoriesIds;
        Tasks = quote.Tasks?.Select(task => new TaskResponse(task));
    }
    
    [Display("Quote ID")]
    public string Id { get; set; }
    
    [Display("Is classic quote")]
    public bool IsClassicQuote { get; set; }
    
    [Display("ID number")]
    public string IdNumber { get; set; }
    
    public string Name { get; set; }
    
    [Display("Customer ID")]
    public string CustomerId { get; set; }
    
    [Display("Contact person ID")]
    public string ContactPersonId { get; set; }

    public string Status { get; set; }
    
    [Display("Start date")]
    public DateTime? StartDate { get; set; }
    
    public DateTime? Deadline { get; set; }
    
    [Display("Created on")]
    public DateTime? CreatedOn { get; set; }
    
    [Display("Offer expiration date")]
    public DateTime? OfferExpiry { get; set; }
    
    public Instructions Instructions { get; set; }
    
    public FinanceInformation Finance { get; set; }

    [Display("Categories IDs")]
    public IEnumerable<string> CategoriesIds { get; set; }

    public IEnumerable<TaskResponse> Tasks { get; set; }
}