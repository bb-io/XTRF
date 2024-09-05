using Apps.XTRF.Shared.Models.Responses.Invoices;
using Blackbird.Applications.Sdk.Common;

public class CustomerInvoiceResponse
{
    [Display("Invoice ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Total gross amount")]
    public double TotalGross { get; set; }

    [Display("Total net amount")]
    public double TotalNetto { get; set; }

    [Display("Currency ID")]
    public string CurrencyId { get; set; } = string.Empty;

    [Display("Status")]
    public string Status { get; set; } = string.Empty;

    [Display("Invoice number")]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Display("Invoice type")]
    public string Type { get; set; } = string.Empty;

    [Display("Tasks value")]
    public double TasksValue { get; set; }

    [Display("VAT calculation rule")]
    public string VatCalculationRule { get; set; } = string.Empty;

    [Display("Total in words")]
    public string TotalInWords { get; set; } = string.Empty;

    [Display("Payment method ID")]
    public string? PaymentMethodId { get; set; }

    [Display("Payment terms")]
    public PaymentTermsResponse PaymentTerms { get; set; } = new();

    [Display("Customer ID")]
    public string CustomerId { get; set; } = string.Empty;

    [Display("Invoice dates")]
    public InvoiceDatesResponse Dates { get; set; } = new();

    [Display("Customer details")]
    public CustomerDetailsResponse CustomerDetails { get; set; } = new();
    
    public List<PaymentResponse> Payments { get; set; } = new();
}

public class PaymentTermsResponse
{
    [Display("Payment term name")]
    public string Name { get; set; } = string.Empty;

    [Display("Description")]
    public string Description { get; set; } = string.Empty;
}

public class BaseDateResponse
{
    [Display("Time in milliseconds")]
    public long Time { get; set; }
}

public class CustomerDetailsResponse
{
    [Display("Customer name")]
    public string Name { get; set; } = string.Empty;

    [Display("VAT UE")]
    public string VatUE { get; set; } = string.Empty;

    [Display("Address line")]
    public string AddressLine { get; set; } = string.Empty;

    [Display("City")]
    public string City { get; set; } = string.Empty;

    [Display("Postal code")]
    public string PostalCode { get; set; } = string.Empty;

    [Display("Country ID")]
    public string CountryId { get; set; } = string.Empty;

    [Display("Country")]
    public string Country { get; set; } = string.Empty;
}
