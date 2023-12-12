﻿using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Requests.Job;

public class UpdateJobDatesRequest
{
    [Display("Start date")]
    public DateTime StartDate { get; set; }
    
    public DateTime Deadline { get; set; }
}