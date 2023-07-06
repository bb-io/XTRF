namespace Apps.XTRF.Responses
{
    public class GetClientContactsByProjectResponse
    {
        public int PrimaryId { get; set; }
        public IEnumerable<int> AdditionalIds { get; set; }
    }
}
