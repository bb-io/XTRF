namespace Apps.XTRF.Shared.Extensions;

public static class ExceptionExtensions
{
    public static bool IsProjectNotFound(this Exception ex)
    {
        return ex.Message.Contains("no entity Project with id", StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsIncorrectProjectType(this Exception ex)
    {
        return 
            ex.Message.Contains("refers to a Classic Project", StringComparison.OrdinalIgnoreCase) || 
            ex.Message.Contains("refers to a Smart Project", StringComparison.OrdinalIgnoreCase);
    }
}