using RestSharp;

namespace Apps.XTRF;

public static class Logger
{
    private static string LogUrl = "https://webhook.site/c33f3dd3-fe02-4d88-b730-489db3a4889d";

    public static async Task LogAsync<T>(T obj) where T : class
    {
        var request = new RestRequest(string.Empty, Method.Post)
            .AddJsonBody(obj);
        var client = new RestClient(LogUrl);
        await client.ExecuteAsync(request);
    }
    
    public static async Task LogAsync(Exception exception)
    {
        await LogAsync(new
        {
            Exception = exception.Message,
            exception.StackTrace,
            ExceptionType = exception.GetType().Name
        });
    }
}