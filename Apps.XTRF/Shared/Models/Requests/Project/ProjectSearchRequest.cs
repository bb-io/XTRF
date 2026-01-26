
using Apps.XTRF.Shared.DataSourceHandlers;
using Apps.XTRF.Shared.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XTRF.Shared.Models.Requests.Project
{
    public class ProjectSearchRequest
    {
        [Display("View ID")]
        public string ViewId { get; set; } = default!;

        [Display("Start date from")]
        public DateTime? StartDateFrom { get; set; }

        [Display("Start date to")]
        public DateTime? StartDateTo { get; set; }

        [Display("Deadline from")]
        public DateTime? DeadlineFrom { get; set; }

        [Display("Deadline to")]
        public DateTime? DeadlineTo { get; set; }

        [Display("Text")]
        public string? Text { get; set; }

        [Display("Status")]
        [StaticDataSource(typeof(ProjectStatusDataHandler))]
        public IEnumerable<string>? Status { get; set; }

        [Display("Client name")]
        public string? ClientName { get; set; }

        [Display("Project manager is")]
        public IEnumerable<string>? ProjectManagerIs { get; set; }

        [Display("Project manager is not")]
        public IEnumerable<string>? ProjectManagerIsNot { get; set; }

        [Display("Project coordinator is")]
        public IEnumerable<string>? ProjectCoordinatorIs { get; set; }

        [Display("Project coordinator is not")]
        public IEnumerable<string>? ProjectCoordinatorIsNot { get; set; }

        [Display("Account manager is")]
        public IEnumerable<string>? AccountManagerIs { get; set; }

        [Display("Account manager is not")]
        public IEnumerable<string>? AccountManagerIsNot { get; set; }

        [Display("Sales person is")]
        public IEnumerable<string>? SalesPersonIs { get; set; }

        [Display("Sales person is not")]
        public IEnumerable<string>? SalesPersonIsNot { get; set; }

        [Display("Source language is")]
        [DataSource(typeof(LanguageDataHandler))]
        public IEnumerable<string>? SourceLanguageIs { get; set; }

        [Display("Source language is not")]
        [DataSource(typeof(LanguageDataHandler))]
        public IEnumerable<string>? SourceLanguageIsNot { get; set; }

        [Display("Target language is")]
        [DataSource(typeof(LanguageDataHandler))]
        public IEnumerable<string>? TargetLanguageIs { get; set; }

        [Display("Target language is not")]
        [DataSource(typeof(LanguageDataHandler))]
        public IEnumerable<string>? TargetLanguageIsNot { get; set; }

        [Display("Task specialization is")]
        public IEnumerable<string>? TaskSpecializationIs { get; set; }

        [Display("Task specialization is not")]
        public IEnumerable<string>? TaskSpecializationIsNot { get; set; }
    }
}
