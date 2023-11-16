﻿using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Requests.Job;

public class UpdateJobDatesRequest
{
    [Display("Job ID")]
    public string JobId { get; set; }
        
    [Display("Start date")]
    public string StartDate { get; set; }
    public string Deadline { get; set; }
}