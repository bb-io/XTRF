using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Responses.Entities;

public class ClassicShortJob
{
    [Display("Job ID")] 
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    [Display("ID number")]
    public string IdNumber { get; set; }
    
    public ClassicJobFiles Files { get; set; }
}

public class ClassicJobFiles
{
    public IEnumerable<ClassicFileXTRF> InputFiles { get; set; }
    public IEnumerable<ClassicFileXTRF> OutputFiles { get; set; }
}