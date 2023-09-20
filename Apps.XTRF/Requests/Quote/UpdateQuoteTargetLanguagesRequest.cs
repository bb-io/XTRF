﻿using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Requests.Quote;

public class UpdateQuoteTargetLanguagesRequest
{
    [Display("Quote ID")]
    public string QuoteId { get; set; }
    
    [Display("Target language IDs")]
    public IEnumerable<int> TargetLanguageIds { get; set; }
}