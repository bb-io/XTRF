using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XTRF.Smart.DataSourceHandlers.EnumHandlers;

public class SmartFileCategoryDataHandler : IStaticDataSourceHandler
{
    private static Dictionary<string, string> EnumValues => new()
    {
        { "BILINGUAL_DOC", "Bilingual document" },
        { "CAT_ANALYSIS", "CAT analysis" },
        { "CAT_PACKAGE", "CAT package" },
        { "CAT_PACKAGE_RETURN", "CAT package (return)" },
        { "FILTERING_RULES", "Filtering rules" },
        { "FORMATTED_DOCUMENT", "Formatted document" },
        { "MEMOQ_LIGHT_RESOURCES", "memoQ light resource" },
        { "OTHER", "Other" },
        { "QA_REPORT", "QA report" },
        { "REFERENCE", "Reference file" },
        { "SEGMENTATION_RULES", "Segmentation rules" },
        { "SOURCE_DOCUMENT", "Source document" },
        { "SOURCE_TO_BE_PREPARED", "Source to be prepared" },
        { "TERMINOLOGY", "Terminology" },
        { "TRANSLATED_DOCUMENT", "Translated document" },
        { "TM", "Translation memory" }
    };

    public Dictionary<string, string> GetData()
    {
        return EnumValues;
    }
}