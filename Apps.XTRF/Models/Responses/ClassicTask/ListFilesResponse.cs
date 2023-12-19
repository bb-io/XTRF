using Apps.XTRF.Models.Responses.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Responses.ClassicTask;

public class ListFilesResponse
{
    public ListFilesResponse(ListFilesResponseWrapper data)
    {
        Workfiles = data.InputFiles.Workfiles;
        Tm = data.InputFiles.Tm;
        Terminology = data.InputFiles.Terminology;
        ReferenceFiles = data.InputFiles.ReferenceFiles;
        LogFiles = data.InputFiles.LogFiles;
        OutputFiles = data.OutputFiles;
    }
    
    public IEnumerable<ClassicFileXTRF> Workfiles { get; set; }
    
    [Display("Translation memory files")]
    public IEnumerable<ClassicFileXTRF> Tm { get; set; }
    
    [Display("Terminology files")]
    public IEnumerable<ClassicFileXTRF> Terminology { get; set; }
    
    [Display("Reference files")]
    public IEnumerable<ClassicFileXTRF> ReferenceFiles { get; set; }
    
    [Display("Log files")]
    public IEnumerable<ClassicFileXTRF> LogFiles { get; set; }
    
    [Display("Output files")]
    public IEnumerable<ClassicFileXTRF> OutputFiles { get; set; }
}

public class ListFilesResponseWrapper
{
    public InputFiles InputFiles { get; set; }
    public IEnumerable<ClassicFileXTRF> OutputFiles { get; set; }
}

public class InputFiles
{
    public IEnumerable<ClassicFileXTRF> Workfiles { get; set; }
    public IEnumerable<ClassicFileXTRF> Tm { get; set; }
    public IEnumerable<ClassicFileXTRF> Terminology { get; set; }
    public IEnumerable<ClassicFileXTRF> ReferenceFiles { get; set; }
    public IEnumerable<ClassicFileXTRF> LogFiles { get; set; }
}