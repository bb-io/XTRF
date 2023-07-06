using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Responses.Models
{
    public class JobDTO
    {
        public string Id { get; set; }
        [Display("Id number")] public string IdNumber { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        [Display("Step number")] public int StepNumber { get; set; }
        [Display("Vendor id")] public int? VendorId { get; set; }
        [Display("Vendor price profile id")] public int? VendorPriceProfileId { get; set; }
        [Display("Step type id")] public string StepTypeId { get; set; }
        [Display("Step type name")] public string StepTypeName { get; set; }
        [Display("Job type id")] public int JobTypeId { get; set; }
        public List<Language> Languages { get; set; }

    }

}
