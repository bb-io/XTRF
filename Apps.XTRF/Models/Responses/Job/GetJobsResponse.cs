using Apps.XTRF.Models.Responses.Entities;

namespace Apps.XTRF.Models.Responses.Job;

public class GetJobsResponse
{
    public IEnumerable<JobDTO> Jobs { get; set; }
}