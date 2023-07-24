namespace Apps.XTRF.Requests.ManageCustomer;

public class CreateCustomerRequest
{
    public string Name { get; }
    public string FullName { get; }
    public CustomerContact Contact { get; }
    

    public CreateCustomerRequest(InputParameters.CreateCustomer input)
    {
        Name = input.Name;
        FullName = input.FullName;
        Contact = new()
        {
            Emails = new()
            {
                Primary = input.Email
            }
        };
    }
}