using Apps.XTRF.InputParameters;

namespace Apps.XTRF.Requests.ManageCustomer;

public class UpdateCustomerRequest
{
    public long Id { get; }
    public string? Name { get; }
    public string? FullName { get; }
    public CustomerContact? Contact { get; }

    public UpdateCustomerRequest(UpdateCustomer input)
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