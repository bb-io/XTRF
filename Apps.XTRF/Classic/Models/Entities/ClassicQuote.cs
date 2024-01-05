using Apps.XTRF.Shared.Models.Entities;

namespace Apps.XTRF.Classic.Models.Entities;

public class ClassicQuote
{
    public string Id { get; set; }
    public bool IsClassicQuote { get; set; }
    public string IdNumber { get; set; }
    public string Name { get; set; }
    public string CustomerId { get; set; }
    public string ContactPersonId { get; set; }
    public string Status { get; set; }
    public ClassicQuoteDates Dates { get; set; }
    public Instructions Instructions { get; set; }
    public FinanceInformation Finance { get; set; }
    public IEnumerable<string> CategoriesIds { get; set; }
    public IEnumerable<ClassicTask>? Tasks { get; set; }
}

public class ClassicQuoteDates
{
    public LongDateTimeRepresentation? StartDate { get; set; }
    public LongDateTimeRepresentation? Deadline { get; set; }
    public LongDateTimeRepresentation? CreatedOn { get; set; }
    public LongDateTimeRepresentation? OfferExpiry { get; set; }
}