using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.XTRF.Shared.Models.Responses.Provider;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Dtos
{
    public class ProviderPersonDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public ContactDto Contact { get; set; } = new();

        public string PositionId { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public bool Active { get; set; } = true;

        public List<string> MotherTonguesIds { get; set; } = new();

        //public List<CustomFieldDto> CustomFields { get; set; } = new();

        public string ProviderId { get; set; } = string.Empty;
    }

    public class CustomFieldDto
    {
        public string Type { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Key { get; set; } = string.Empty;

        public string? Value { get; set; }
    }
}
