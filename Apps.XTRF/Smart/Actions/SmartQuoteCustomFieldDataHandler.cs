using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Shared.Models.Entities.Enums;
using Apps.XTRF.Smart.Models.Requests.SmartQuote;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XTRF.Smart.Actions;

public class SmartQuoteCustomFieldDataHandler([ActionParameter] SmartQuoteIdentifier quote, InvocationContext invocationContext)
    : BaseQuoteCustomFieldDataHandler(quote.QuoteId, ApiType.Smart, invocationContext), IAsyncDataSourceItemHandler
{
}
