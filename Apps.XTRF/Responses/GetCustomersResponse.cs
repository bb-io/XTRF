using Apps.XTRF.Responses.Models;

namespace Apps.XTRF.Responses
{
    public class GetCustomersResponse
    {
        public IEnumerable<SimpleCustomer> SimpleCustomers { get; set; }
    }
}
