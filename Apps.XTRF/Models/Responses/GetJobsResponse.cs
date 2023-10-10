using Apps.XTRF.Models.Responses.Models;

namespace Apps.XTRF.Models.Responses;

public class GetJobsResponse
{
    public IEnumerable<JobDTO> Jobs { get; set; }
}