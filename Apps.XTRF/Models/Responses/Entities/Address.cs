namespace Apps.XTRF.Models.Responses.Entities;

public class Address
{
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public int CountryId { get; set; }
}