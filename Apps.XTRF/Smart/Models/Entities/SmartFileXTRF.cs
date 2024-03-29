﻿using Apps.XTRF.Shared.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Entities;

public class SmartFileXTRF
{
    [Display("File ID")] 
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public long? Size { get; set; }
    
    [Display("Category key")] 
    public string CategoryKey { get; set; }
    
    [Display("Languages information")] 
    public LanguageRelation LanguageRelation { get; set; }
    
    public IEnumerable<string> SharedWithJobs { get; set; }
}

public class LanguageRelation
{
    public IEnumerable<string> Languages { get; set; }
    
    [Display("Language combinations")] 
    public IEnumerable<LanguageCombination> LanguageCombinations { get; set; }
}
