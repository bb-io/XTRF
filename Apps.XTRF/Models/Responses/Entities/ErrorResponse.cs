namespace Apps.XTRF.Models.Responses.Entities;

public class ErrorResponse
{
    public int Status { get; set; }
    public string ErrorMessage { get; set; }
    public string? DetailedMessage { get; set; }
}