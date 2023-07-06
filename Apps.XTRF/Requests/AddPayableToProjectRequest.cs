namespace Apps.XTRF.Requests
{
    public class AddPayableToProjectRequest
    {
        public string ProjectId { get; set; }
        public string JobId { get; set; }
        public string RateOrigin { get; set; }
        public int CurrencyId { get; set; }

    }
}
