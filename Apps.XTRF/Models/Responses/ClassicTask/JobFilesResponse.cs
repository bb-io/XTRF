using Apps.XTRF.Models.Responses.Entities;

namespace Apps.XTRF.Models.Responses.ClassicTask;

public record JobFilesResponse(IEnumerable<ClassicShortJob> Jobs);