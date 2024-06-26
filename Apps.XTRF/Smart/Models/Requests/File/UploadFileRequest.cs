﻿using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Shared.Models;
using Apps.XTRF.Smart.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Smart.Models.Requests.File;

public class UploadFileRequest : FileWrapper
{
    [StaticDataSource(typeof(SmartFileCategoryDataHandler))]
    public string Category { get; set; }   
    
    [Display("Language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string? LanguageId { get; set; }
    
    [Display("Source language of language combination")]
    [DataSource(typeof(LanguageDataHandler))]
    public string? SourceLanguageId { get; set; }
    
    [Display("Target language of language combination")]
    [DataSource(typeof(LanguageDataHandler))]
    public string? TargetLanguageId { get; set; }
}