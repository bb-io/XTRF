using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Responses.Models;

public class JobDTO
{
    [Display("ID")] public string Id { get; set; }
    [Display("ID number")] public string IdNumber { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    [Display("Step number")] public int StepNumber { get; set; }
    [Display("Vendor ID")] public string? VendorId { get; set; }
    [Display("Vendor price profile ID")] public string? VendorPriceProfileId { get; set; }
    [Display("Step type ID")] public string StepTypeId { get; set; }
    [Display("Step type name")] public string StepTypeName { get; set; }
    [Display("Job type ID")] public string JobTypeId { get; set; }
    public List<JobLanguage> Languages { get; set; }

    public JobDTO()
    {
    }

    public JobDTO(JobResponse response)
    {
        Id = response.Id;
        IdNumber = response.IdNumber;
        Name = response.Name;
        Status = response.Status;
        StepNumber = response.StepNumber;
        VendorId = response.VendorId.ToString();
        VendorPriceProfileId = response.VendorPriceProfileId.ToString();
        StepTypeId = response.StepType.Id;
        StepTypeName = response.StepType.Name;
        JobTypeId = response.StepType.JobTypeId.ToString();
        Languages = response.Languages;
    }
}