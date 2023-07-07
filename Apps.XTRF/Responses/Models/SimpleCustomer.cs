using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Responses.Models
{
    public class SimpleCustomer
    {
        public SimpleCustomer()
        {
            Child = new ChildCustomer()
            {
                Id = 1234,
                Name = "Test child name",
            };
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ChildCustomer Child { get; set; }
    }

    public class ChildCustomer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
