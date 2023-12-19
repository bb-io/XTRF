namespace Apps.XTRF.Models.Responses.Entities;

public class ClassicDates
{
    public StartDate? StartDate { get; set; }
    public Deadline? Deadline { get; set; }
    public ActualStartDate? ActualStartDate { get; set; }
    public ActualDeliveryDate? ActualDeliveryDate { get; set; }
}

public class StartDate
{
    public long? Time { get; set; }
}
    
public class Deadline
{
    public long? Time { get; set; }
}
    
public class ActualStartDate
{
    public long? Time { get; set; }
}
    
public class ActualDeliveryDate
{
    public long? Time { get; set; }
}