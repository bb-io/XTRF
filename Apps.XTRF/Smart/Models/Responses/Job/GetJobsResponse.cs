namespace Apps.XTRF.Smart.Models.Responses.Job;

public class GetJobsResponse
{
    public IEnumerable<Entities.Job> Jobs { get; set; }
}