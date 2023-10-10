namespace Apps.XTRF.Models.Responses;

public class JobResponse
{
    public string Id { get; set; }
    public string IdNumber { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public int StepNumber { get; set; }
    public int? VendorId { get; set; }
    public int? VendorPriceProfileId { get; set; }
    public StepType StepType { get; set; }
    public List<JobLanguage> Languages { get; set; }

}

public class StepType
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int JobTypeId { get; set; }
}

public class JobLanguage
{
    public int SourceLanguageId { get; set; }
    public int TargetLanguageId { get; set; }
    public int SpecializationId { get; set; }

}