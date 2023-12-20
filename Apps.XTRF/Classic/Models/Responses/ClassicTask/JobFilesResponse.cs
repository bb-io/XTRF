using Apps.XTRF.Classic.Models.Entities;

namespace Apps.XTRF.Classic.Models.Responses.ClassicTask;

public record JobFilesResponse(IEnumerable<ClassicShortJob> Jobs);