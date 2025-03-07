using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Responses.Provider.Persons
{
    public class ProviderPersonResponse
    {
        [Display("Person ID")]
        public string Id { get; set; } = string.Empty;
        
        [Display("First name")]
        public string Name { get; set; } = string.Empty;
        
        [Display("Last name")]
        public string LastName { get; set; } = string.Empty;

        [Display("Contact information")]
        public ContactResponse Contact { get; set; } = new();

        [Display("Position ID")]
        public string PositionId { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public bool Active { get; set; } = true;

        [Display("Mother tongue IDs")]
        public List<string> MotherTonguesIds { get; set; } = new();

        [Display("Custom fields")]
        public List<CustomFieldResponse> CustomFields { get; set; } = new();

        [Display("Provider ID")]
        public string ProviderId { get; set; } = string.Empty;
    }

    public class CustomFieldResponse
    {
        public string Type { get; set; } = string.Empty;

        [Display("Field name")]
        public string Name { get; set; } = string.Empty;

        [Display("Field key")]
        public string Key { get; set; } = string.Empty;

        public List<string?>? Value { get; set; }
    }
}