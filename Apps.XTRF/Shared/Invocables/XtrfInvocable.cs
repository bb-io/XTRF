using Apps.XTRF.Shared.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XTRF.Shared.Invocables;

public class XtrfInvocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected XtrfClient Client { get; }

    public XtrfInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new(Creds);
    }

    protected long? ConvertToInt64(string? input, string parameterName)
    {
        if (input == null)
            return null;
        
        var isSuccessful = long.TryParse(input, out var result);

        if (!isSuccessful)
            throw new Exception($"{parameterName} must be a valid number.");

        return result;
    }
    
    protected IEnumerable<long>? ConvertToInt64Enumerable(IEnumerable<string>? input, string parameterName)
    {
        var result = input?.Select(value =>
            long.TryParse(value, out var longValue)
                ? longValue
                : throw new Exception($"{parameterName} must contain valid numbers.")).ToArray();
        
        return result;
    }
}