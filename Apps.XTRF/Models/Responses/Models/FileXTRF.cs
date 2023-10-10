using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Responses.Models;

public class FileXTRF
{
    [Display("ID")] public string Id { get; set; }
    public string Name { get; set; }
    public long? Size { get; set; }
    [Display("Category key")] public string CategoryKey { get; set; }
    [Display("Language relation")] public LanguageRelation LanguageRelation { get; set; }

    public IEnumerable<int> Languages => LanguageRelation.Languages;

    [Display("Language combinations")]
    public IEnumerable<LanguageCombination> LanguageCombinations => LanguageRelation.LanguageCombinations;
}