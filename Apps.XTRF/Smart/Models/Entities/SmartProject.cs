namespace Apps.XTRF.Smart.Models.Entities;

public class SmartProject
{
    public string Id { get; set; }
    public bool IsClassicProject { get; set; }
    public string QuoteIdNumber { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string BudgetCode { get; set; }
    public string ClientId { get; set; }
    public string ServiceId { get; set; }
    public string Origin { get; set; }
    public string ClientReferenceNumber { get; set; }
    public string ClientNotes { get; set; }
    public string InternalNotes { get; set; }
    public string ProjectIdNumber { get; set; }
    public string InstructionsForAllJobs { get; set; }
    public long? ClientDeadline { get; set; }
    public long? OrderedOn { get; set; }
    public SmartProjectLanguages Languages { get; set; }
    public SmartProjectDocuments Documents { get; set; }
    public SmartProjectPeople People { get; set; }
}

public class SmartProjectLanguages
{
    public string SourceLanguageId { get; set; }
    public IEnumerable<string> TargetLanguageIds { get; set; }
    public string SpecializationId { get; set; }
    public IEnumerable<SmartJobLanguageCombination> LanguageCombinations { get; set; }
}

public class SmartProjectDocuments
{
    public string ProjectConfirmationStatus { get; set; }
}

public class SmartProjectPeople
{
    public string ProjectManagerId { get; set; }
}