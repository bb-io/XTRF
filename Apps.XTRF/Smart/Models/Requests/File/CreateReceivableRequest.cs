using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;
using Apps.XTRF.Shared.Models;
using Apps.XTRF.Smart.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Requests.File;

public class CreateFinanceRequest : FileWrapper
{
    [Display("Project ID")]
    public string ProjectId { get; set; }
    
    [Display("Source language of language combination")]
    [DataSource(typeof(LanguageDataHandler))]
    public string? SourceLanguageId { get; set; }
    
    [Display("Target language of language combination")]
    [DataSource(typeof(LanguageDataHandler))]
    public string? TargetLanguageId { get; set; }

    [Display("File name")]
    public string? FileName { get; set; }

    [Display("Job type ID"), DataSource(typeof(JobTypeDataHandler))]
    public string JobType { get; set; }

    [Display("Rate origin"), StaticDataSource(typeof(RateOriginDataHandler))]
    public string? RateOrigin { get; set; }
    
    [Display("Currency ID")]
    public string? CurrencyId { get; set; }
    
    [Display("Total")]
    public string? Total { get; set; }

    [Display("Invoice ID")]
    public string? InvoiceId { get; set; }
    
    [Display("Type"), StaticDataSource(typeof(FinanceTypeDataHandler))]
    public string? Type { get; set; }
    
    [Display("Calculation unit ID")]
    public string CalculationUnitId { get; set; }

    [Display("Ignore minimum change")]
    public bool? IgnoreMinimumChange { get; set; }

    [Display("Minimum charge")]
    public int? MinimumCharge { get; set; }

    [Display("Description")]
    public string? Description { get; set; }

    [Display("Rate")]
    public string? Rate { get; set; }

    public int? Quantity { get; set; }
}

public class CreateReceivableRequest : CreateFinanceRequest
{
    [Display("Receivable ID")]
    public string? Id { get; set; }
    
    [Display("Task ID")]
    public string? TaskId { get; set; }
}

public class CreatePayableRequest : CreateFinanceRequest
{
    [Display("Payable ID")]
    public string? Id { get; set; }
    
    [Display("Job ID"), DataSource(typeof(SmartJobDataHandler))]
    public string JobId { get; set; }
}