using Apps.XTRF.Classic.Models.Entities;
using Apps.XTRF.Classic.Models.Identifiers;
using Apps.XTRF.Classic.Models.Requests.ClassicQuote;
using Apps.XTRF.Classic.Models.Responses.ClassicQuote;
using Apps.XTRF.Shared.Api;
using Apps.XTRF.Shared.Constants;
using Apps.XTRF.Shared.Invocables;
using Apps.XTRF.Shared.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.XTRF.Classic.Actions;

[ActionList]
public class ClassicQuoteActions : XtrfInvocable
{
    public ClassicQuoteActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    #region Get

    [Action("Classic: Get quote", Description = "Get information about classic quote")]
    public async Task<QuoteResponse> GetQuote([ActionParameter] QuoteIdentifier quoteIdentifier)
    {
        var request = new XtrfRequest($"/quotes/{quoteIdentifier.QuoteId}", Method.Get, Creds);
        var quote = await Client.ExecuteWithErrorHandling<ClassicQuote>(request);
        return new(quote);
    }

    #endregion

    #region Post

    [Action("Classic: Create language combination for quote", Description = "Create a new language combination for a " +
                                                                            "classic quote without creating a task")]
    public async Task<QuoteIdentifier> CreateLanguageCombinationForProject(
        [ActionParameter] QuoteIdentifier quoteIdentifier, 
        [ActionParameter] LanguageCombinationIdentifier languageCombination)
    {
        var request = new XtrfRequest($"/quotes/{quoteIdentifier.QuoteId}", Method.Post, Creds)
            .WithJsonBody(new
            {
                sourceLanguageId = ConvertToInt64(languageCombination.SourceLanguageId, "Source language"),
                targetLanguagesIds = ConvertToInt64(languageCombination.TargetLanguageId, "Target language")
            });
        await Client.ExecuteWithErrorHandling(request);
        return quoteIdentifier;
    }

    [Action("Classic: Start quote", Description = "Start a classic quote")]
    public async Task<QuoteIdentifier> StartQuote([ActionParameter] QuoteIdentifier quoteIdentifier)
    {
        var request = new XtrfRequest($"/quotes/{quoteIdentifier.QuoteId}/start", Method.Post, Creds);
        await Client.ExecuteWithErrorHandling(request);
        return quoteIdentifier;
    }
    
    [Action("Classic: Send quote for customer confirmation", Description = "Send a quote for customer confirmation, " +
                                                                           "changing the quote status to \"Sent\"")]
    public async Task<QuoteIdentifier> SendQuoteForConfirmation([ActionParameter] QuoteIdentifier quoteIdentifier)
    {
        var request = new XtrfRequest($"/quotes/{quoteIdentifier.QuoteId}/confirmation/send", Method.Post, Creds);
        await Client.ExecuteWithErrorHandling(request);
        return quoteIdentifier;
    }

    #endregion

    #region Put

    [Action("Classic: Update quote instructions", Description = "Update instructions of a given quote, specifying " +
                                                                "only the fields that require updating")]
    public async Task<QuoteIdentifier> UpdateQuoteInstructions([ActionParameter] QuoteIdentifier quoteIdentifier, 
        [ActionParameter] UpdateQuoteInstructionsRequest input)
    {
        var request = new XtrfRequest($"/quotes/{quoteIdentifier.QuoteId}/instructions", Method.Put, Creds)
            .WithJsonBody(new
            {
                fromCustomer = input.InstructionFromCustomer,
                forProvider = input.InstructionForProvider,
                Internal = input.InternalInstruction,
                paymentNoteForCustomer = input.PaymentNoteForCustomer,
                notes = input.Notes
            }, JsonConfig.Settings);
        
        await Client.ExecuteWithErrorHandling(request);
        return quoteIdentifier;
    }

    #endregion

    #region Delete

    [Action("Classic: Delete quote", Description = "Delete a quote")]
    public async Task DeleteQuote([ActionParameter] QuoteIdentifier quoteIdentifier)
    {
        var request = new XtrfRequest($"/quotes/{quoteIdentifier.QuoteId}", Method.Delete, Creds);
        await Client.ExecuteWithErrorHandling(request);
    }

    #endregion
}