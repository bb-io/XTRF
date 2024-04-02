using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Responses.File;

public class UploadedFinanceFileResponse
{
    [Display("ID")]
    public int Id { get; set; }

    [Display("Job type ID")]
    public string JobTypeId { get; set; }

    [Display("Language combination ID number")]
    public string LanguageCombinationIdNumber { get; set; }

    [Display("Rate origin")]
    public string RateOrigin { get; set; }

    [Display ("Currency ID")]
    public string CurrencyId { get; set; }

    [Display ("Total")]
    public int Total { get; set; }

    [Display("Invoice ID")]
    public string InvoiceId { get; set; }

    [Display("Type")]
    public string Type { get; set; }

    [Display("Calculation unit ID")]
    public string CalculationUnitId { get; set; }

    [Display("Ignore minimum charge")]
    public bool IgnoreMinimumCharge { get; set; }

    [Display("Minimum charge")]
    public int MinimumCharge { get; set; }

    [Display("Description")]
    public string Description { get; set; }

    [Display("Rate")]
    public int Rate { get; set; }

    [Display("Quantity")]
    public int Quantity { get; set; }

    [Display("Source language ID")]
    public string SourceLanguageId { get; set; }
    
    [Display("Target language ID")]
    public string TargetLanguageId { get; set; }

    public UploadedFinanceFileResponse(UploadedFinanceFileDto dto)
    {
        Id = dto.Id;
        JobTypeId = dto.JobTypeId.ToString();
        LanguageCombinationIdNumber = dto.LanguageCombinationIdNumber;
        RateOrigin = dto.RateOrigin;
        CurrencyId = dto.CurrencyId.ToString();
        Total = dto.Total;
        InvoiceId = dto.InvoiceId;
        Type = dto.Type;
        CalculationUnitId = dto.CalculationUnitId.ToString();
        IgnoreMinimumCharge = dto.IgnoreMinimumCharge;
        MinimumCharge = dto.MinimumCharge;
        Description = dto.Description;
        Rate = dto.Rate;
        Quantity = dto.Quantity;
        SourceLanguageId = dto.LanguageCombination.SourceLanguageId.ToString();
        TargetLanguageId = dto.LanguageCombination.TargetLanguageId.ToString();
    }
}

public class UploadedFinanceFileDto
{
    public int Id { get; set; }
    public int JobTypeId { get; set; }
    public string LanguageCombinationIdNumber { get; set; }
    public string RateOrigin { get; set; }
    public int CurrencyId { get; set; }
    public int Total { get; set; }

    public string InvoiceId { get; set; }

    public string Type { get; set; }

    public int CalculationUnitId { get; set; }

    public bool IgnoreMinimumCharge { get; set; }

    public int MinimumCharge { get; set; }

    public string Description { get; set; }

    public int Rate { get; set; }

    public int Quantity { get; set; }
    
    public LanguageCombinationDto LanguageCombination { get; set; }
}

public class LanguageCombinationDto
{
    public int SourceLanguageId { get; set; }
    
    public int TargetLanguageId { get; set; }
}