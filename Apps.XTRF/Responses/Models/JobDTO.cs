namespace Apps.XTRF.Responses.Models
{
    public class JobDTO
    {
        public string Id { get; set; }
        public string IdNumber { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int StepNumber { get; set; }
        public int? VendorId { get; set; }
        public int? VendorPriceProfileId { get; set; }
        public string StepTypeId { get; set; }
        public string StepTypeName { get; set; }
        public int JobTypeId { get; set; }
        public List<Language> Languages { get; set; }

    }

}
