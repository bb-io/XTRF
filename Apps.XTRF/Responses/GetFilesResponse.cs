﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.XTRF.Responses.Models;

namespace Apps.XTRF.Responses
{
    public class GetFilesResponse
    {
        public IEnumerable<FileXTRF> Files { get; set; }
    }
}
