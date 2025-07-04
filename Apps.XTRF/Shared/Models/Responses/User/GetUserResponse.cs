

using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Responses.User
{
    public class GetUserResponse
    {
        [Display("Fisrt name")]
        public string? firstName { get; set; }

        [Display("Last name")]
        public string? lastName { get; set; }

        [Display("E-mail")]
        public string? email { get; set; }

        [Display("User group name")]
        public string? userGroupName { get; set; }

        [Display("Position")]
        public string? positionName { get; set; }

    }
}
