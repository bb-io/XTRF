﻿using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Responses
{
    public class CheckForCatToolResponse
    {
        [Display("Project created int cat tool or creation is queued")]
        public bool ProjectCreatedInCatToolOrCreationIsQueued { get; set; }
    }
}