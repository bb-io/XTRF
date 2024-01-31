using Apps.XTRF.Smart.Models.Entities;

namespace Apps.XTRF.Smart.Models.Responses.File;

public record ListFilesResponse(IEnumerable<SmartFileXTRF> Files);