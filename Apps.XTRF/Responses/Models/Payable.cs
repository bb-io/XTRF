﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Responses.Models
{
    public class Payable
    {
        public int Id { get; set; }
        public int JobTypeId { get; set; }
        public int CurrencyId { get; set; }
        public double Total { get; set; }
        public string Type { get; set; }
        public double Quantity { get; set; }
        public string JobId { get; set; }
    }
}