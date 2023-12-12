using Apps.XTRF.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Models.Identifiers;

public class PersonIdentifier
{
    [Display("Person")]
    [DataSource(typeof(PersonDataHandler))]
    public string PersonId { get; set; }
}