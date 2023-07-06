using Apps.XTRF.Responses.Models;

namespace Apps.XTRF
{
    public static class ExtensionMethods
    {
        public static long ConvertToUnixTime(this string inputDate)
        {
            DateTime date = DateTime.Parse(inputDate).ToUniversalTime();
            var unspecifiedDateKind = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);

            DateTimeOffset offset = new DateTimeOffset(unspecifiedDateKind);
            long unixTime = offset.ToUnixTimeMilliseconds();

            return unixTime;
        }

        public static JobDTO MapJobResponseToDTO(JobResponse response)
        {
            var jobDTO = new JobDTO()
            {
                Id = response.Id,
                IdNumber = response.IdNumber,
                Name = response.Name,
                Status = response.Status,
                StepNumber = response.StepNumber,
                VendorId = response.VendorId,
                VendorPriceProfileId = response.VendorPriceProfileId,
                StepTypeId = response.StepType.Id,
                StepTypeName = response.StepType.Name,
                JobTypeId = response.StepType.JobTypeId,
                Languages = response.Languages
            };

            return jobDTO;
        }
    }
}
