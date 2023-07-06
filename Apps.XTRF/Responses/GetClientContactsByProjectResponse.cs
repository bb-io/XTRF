using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Responses
{
    public class GetClientContactsByProjectResponse
    {
        [Display("Primary id")]  public int PrimaryId { get; set; }
        [Display("Additional ids")]  public IEnumerable<int> AdditionalIds { get; set; }
    }
}
