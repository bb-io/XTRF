using Apps.XTRF.Classic.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Classic.Models.Identifiers;

public class ClassicFileIdentifier
{
    [Display("File")] 
    [DataSource(typeof(ClassicFileDataHandler))]
    public string FileId { get; set; }
}