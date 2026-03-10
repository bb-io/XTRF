namespace Apps.XTRF.Classic.Models.Responses
{
    public class ProjectGroupDto
    {
        public string? Id { get; set; }
        public string? GroupName { get; set; }
        public string? GroupNumber { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }

        public IEnumerable<long>? CustomersIds { get; set; }
        public IEnumerable<long>? LinkedProjectsIds { get; set; }
        public IEnumerable<string>? LinkedSmartProjectsIds { get; set; }
        public IEnumerable<long>? LinkedQuotesIds { get; set; }
        public IEnumerable<string>? LinkedSmartQuotesIds { get; set; }
        public IEnumerable<long>? CategoriesIds { get; set; }

        public ProjectGroupFinancesDto? Finances { get; set; }
    }

    public class ProjectGroupFinancesDto
    {
        public ProjectGroupCurrencyDto? Currency { get; set; }
        public ProjectGroupBudgetDto? Budget { get; set; }

        public decimal? TotalPayables { get; set; }
        public decimal? TotalReceivables { get; set; }
        public decimal? Profit { get; set; }
        public string? Margin { get; set; }
        public string? ROI { get; set; }
    }

    public class ProjectGroupCurrencyDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public string? IsoCode { get; set; }
    }

    public class ProjectGroupBudgetDto
    {
        public decimal? Value { get; set; }
        public string? UsagePercentageValue { get; set; }
    }
}
