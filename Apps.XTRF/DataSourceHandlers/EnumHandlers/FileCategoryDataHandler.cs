using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.XTRF.DataSourceHandlers.EnumHandlers;

public class FileCategoryDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "BILINGUAL_DOCUMENT", "Bilingual document" },
        { "CAT_ANALYSIS", "CAT analysis" },
        { "CAT_PACKAGE", "CAT package" },
        { "CAT_PACKAGE_RETURN", "CAT package (return)" },
        { "FILTERING_RULES", "Filtering rules" },
        { "FORMATTED_DOCUMENT", "Formatted document" },
        { "MEMOQ_LIGHT_RESOURCE", "memoQ light resource" },
        { "OTHER", "Other" },
        { "QA_REPORT", "QA report" },
        { "REFERENCE_FILE", "Reference file" },
        { "SEGMENTATION_RULES", "Segmentation rules" },
        { "SOURCE_DOCUMENT", "Source document" },
        { "SOURCE_TO_BE_PREPARED", "Source to be prepared" },
        { "TERMINOLOGY", "Terminology" },
        { "TRANSLATED_DOCUMENT", "Translated document" },
        { "TRANSLATION_MEMORY", "Translation memory" }
    };
}