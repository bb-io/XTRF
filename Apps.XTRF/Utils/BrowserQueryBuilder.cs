using Apps.XTRF.Shared.Models.Requests.Project;

namespace Apps.XTRF.Utils
{
    public static class BrowserQueryBuilder
    {
        public static Dictionary<string, string> BuildProjectFilters(ProjectSearchRequest input)
        {
            var q = new Dictionary<string, string>();

            AddDateFilter(q, "q.startDate", input.StartDateFrom, input.StartDateTo);
            AddDateFilter(q, "q.deadline", input.DeadlineFrom, input.DeadlineTo);

            AddTextFilter(q, "q.projectTextProperty", input.Text);
            AddTextFilter(q, "q.customerName", input.ClientName);

            AddSelectionFilter(q, "q.status", anyOf: input.Status);

            AddSelectionFilter(q, "q.projectManager", anyOf: input.ProjectManagerIs, noneOf: input.ProjectManagerIsNot);
            AddSelectionFilter(q, "q.projectCoordinator", anyOf: input.ProjectCoordinatorIs, noneOf: input.ProjectCoordinatorIsNot);
            AddSelectionFilter(q, "q.accountManager", anyOf: input.AccountManagerIs, noneOf: input.AccountManagerIsNot);
            AddSelectionFilter(q, "q.salesPerson", anyOf: input.SalesPersonIs, noneOf: input.SalesPersonIsNot);

            AddSelectionFilter(q, "q.sourceLanguage", anyOf: input.SourceLanguageIs, noneOf: input.SourceLanguageIsNot);
            AddSelectionFilter(q, "q.targetLanguage", anyOf: input.TargetLanguageIs, noneOf: input.TargetLanguageIsNot);

            AddSelectionFilter(q, "q.taskSpecialization", anyOf: input.TaskSpecializationIs, noneOf: input.TaskSpecializationIsNot);

            return q;
        }

        private static void AddTextFilter(Dictionary<string, string> q, string key, string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return;

            q[key] = $"ilike({value.Trim()})";
        }

        private static void AddSelectionFilter(
            Dictionary<string, string> q,
            string key,
            IEnumerable<string>? anyOf = null,
            IEnumerable<string>? noneOf = null)
        {
            var anyList = anyOf?.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).Distinct().ToArray();
            if (anyList?.Length > 0)
                q[key] = $"anyOf({string.Join(";", anyList)})";

            var noneList = noneOf?.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).Distinct().ToArray();
            if (noneList?.Length > 0)
                q[key] = $"noneOf({string.Join(";", noneList)})";
        }

        private static void AddDateFilter(Dictionary<string, string> q, string key, DateTime? from, DateTime? to)
        {
            if (from is null && to is null)
                return;

            if (from is not null && to is not null)
            {
                q[key] = $"fromTo({ToEpochMs(from.Value)};{ToEpochMs(to.Value)})";
                return;
            }

            if (from is not null)
            {
                q[key] = $"from({ToEpochMs(from.Value)})";
                return;
            }

            q[key] = $"to({ToEpochMs(to!.Value)})";
        }

        private static long ToEpochMs(DateTime dt)
        {
            var utc = dt.Kind == DateTimeKind.Utc ? dt : dt.ToUniversalTime();
            return (long)(utc - DateTime.UnixEpoch).TotalMilliseconds;
        }
    }
}
