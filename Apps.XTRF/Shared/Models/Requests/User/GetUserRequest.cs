using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Requests.User
{
    public class GetUserRequest
    {
        [Display("User ID")]
        public string UserID { get; set; }
    }
}
