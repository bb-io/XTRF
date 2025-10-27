using Apps.XTRF.Smart.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Responses.File;

public record ListFilesResponse(IEnumerable<SmartFileXTRF> Files)
{
    [Display("File names")]
    public IEnumerable<string> FileNames => Files.Select(f => f.Name);
};