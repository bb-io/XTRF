using Apps.XTRF.Shared.Models.Entities;

namespace Apps.XTRF.Classic.Models.Entities;

public class ClassicDates
{
    public LongDateTimeRepresentation? StartDate { get; set; }
    public LongDateTimeRepresentation? Deadline { get; set; }
    public LongDateTimeRepresentation? ActualStartDate { get; set; }
    public LongDateTimeRepresentation? ActualDeliveryDate { get; set; }
}