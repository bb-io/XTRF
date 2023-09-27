﻿using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Responses.Models;

public class LanguageRelation
{
    public IEnumerable<int> Languages { get; set; }
    
    [Display("Language combinations")] 
    public IEnumerable<LanguageCombination> LanguageCombinations { get; set; }
}