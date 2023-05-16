﻿using Apps.XTRF.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Requests
{
    public class ShareFilesWithJobRequest
    {
        public string JobId { get; set; }
        public IEnumerable<string> Files { get; set; }
    }
}
