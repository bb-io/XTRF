using Apps.XTRF.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Requests
{
    public class UploadFileToProjectRequest
    {
        public string ProjectId { get; set; }

        public byte[] File { get; set; }

        public string FileName { get; set; }

        public string Category { get; set; }    
    }
}
