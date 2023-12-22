using Apps.XTRF.Shared.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Smart.Models.Entities;

public class SmartJobLanguageCombination : LanguageCombination
{
    [Display("Specialization ID")]
    public string SpecializationId { get; set; }
}