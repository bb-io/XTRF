using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Shared.Models.Responses.Provider;

public class SendInvitationToProviderResponse
{
    [Display("Providers with invited person count")]
    public double ProvidersWithInvitedPersonCount { get; set; }
    
    [Display("Providers with already registered person count")]
    public double ProvidersWithAlreadyRegisteredPersonCount { get; set; }
    
    [Display("Providers without person count")]
    public double ProvidersWithoutPersonCount { get; set; }
    
    [Display("Invited persons count")]
    public double InvitedPersonsCount { get; set; }
    
    [Display("Already registered persons count")]
    public double AlreadyRegisteredPersonsCount { get; set; }
}
