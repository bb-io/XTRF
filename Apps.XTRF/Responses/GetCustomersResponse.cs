﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Responses
{
    public class GetCustomersResponse
    {
        public IEnumerable<SimpleCustomer> SimpleCustomers { get; set; }
    }
}
