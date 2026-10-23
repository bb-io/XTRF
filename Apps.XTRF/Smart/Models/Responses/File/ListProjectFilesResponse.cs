using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Responses.File;

public record ListProjectFilesResponse(IEnumerable<ProjectFileResponse> Files)
{
    [Display("File names")]
    public IEnumerable<string> FileNames => Files.Select(f => f.Name);
};
