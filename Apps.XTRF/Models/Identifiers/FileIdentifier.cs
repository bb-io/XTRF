using Apps.XTRF.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Models.Identifiers;

public class FileIdentifier
{
    [Display("File ID")] 
    public string FileId { get; set; }
}

public class ClassicFileIdentifier
{
    [Display("File ID")] 
    [DataSource(typeof(ClassicFileDataHandler))]
    public string FileId { get; set; }
}