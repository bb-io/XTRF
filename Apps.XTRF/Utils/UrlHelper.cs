namespace Apps.XTRF.Utils;

public class UrlHelper
{
    public static string BuildCustomerUrl(string url)
    {
        if (url.EndsWith("/"))
        {
            return url + "customer-api";
        }
        
        return url + "/customer-api";
    }
    
    public static string BuildCustomerRequestUrl(string baseUrl, string endpoint, string jsessionCookie)
    {
        var uriBuilder = new UriBuilder(baseUrl + endpoint);
        var path = uriBuilder.Path;
        var query = uriBuilder.Query;

        path += $";jsessionid={jsessionCookie}";
        uriBuilder.Path = path;
        if (!string.IsNullOrEmpty(query))
        {
            uriBuilder.Query = query.TrimStart('?');
        }

        var updatedEndpoint = uriBuilder.ToString();
        return updatedEndpoint;
    }
}