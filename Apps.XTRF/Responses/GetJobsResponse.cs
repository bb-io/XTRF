using Apps.XTRF.Responses.Models;

namespace Apps.XTRF.Responses;

public class GetJobsResponse
{
    public IEnumerable<JobDTO> Jobs { get; set; }
}