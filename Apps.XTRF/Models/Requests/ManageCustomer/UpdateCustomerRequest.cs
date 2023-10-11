using Apps.XTRF.Models.Requests.Customer;

namespace Apps.XTRF.Models.Requests.ManageCustomer;

public class UpdateCustomerRequest
{
    public long Id { get; }
    public string? Name { get; }
    public string? FullName { get; }
    public CustomerContact? Contact { get; }

    public UpdateCustomerRequest(UpdateCustomerInput input)
    {
        if (!long.TryParse(input.CustomerId, out var customerId))
            throw new("Customer ID should be a number");

        Id = customerId;
        Name = input.Name;
        FullName = input.FullName;
        Contact = !string.IsNullOrEmpty(input.Email)
            ? new()
            {
                Emails = new()
                {
                    Primary = input.Email
                }
            }
            : default;
    }
}