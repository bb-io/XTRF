using Apps.XTRF.Classic.DataSourceHandlers;
using Apps.XTRF.Shared.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Shared.Models.Identifiers;

public class PersonIdentifier
{
    [Display("Person ID"), DataSource(typeof(ClassicPersonDataSource))]
    public string PersonId { get; set; }
}