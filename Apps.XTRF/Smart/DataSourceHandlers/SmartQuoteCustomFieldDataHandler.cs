using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Shared.Models.Entities.Enums;
using Apps.XTRF.Shared.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XTRF.Smart.DataSourceHandlers;

public class SmartQuoteCustomFieldDataHandler([ActionParameter] QuoteIdentifier quote, InvocationContext invocationContext)
    : BaseQuoteCustomFieldDataHandler(quote.QuoteId, ApiType.Smart, invocationContext), IAsyncDataSourceItemHandler
{
}
