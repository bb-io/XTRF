namespace Apps.XTRF.Requests
{
    public class UpdateJobDatesRequest
    {
        public string JobId { get; set; }
        public string StartDate { get; set; }
        public string Deadline { get; set; }
    }
}
