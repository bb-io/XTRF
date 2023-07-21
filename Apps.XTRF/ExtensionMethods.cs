using Apps.XTRF.Responses;
using Apps.XTRF.Responses.Models;

namespace Apps.XTRF
{
    public static class ExtensionMethods
    {
        public static long ConvertToUnixTime(this string inputDate)
        {
            var date = DateTime.Parse(inputDate).ToUniversalTime();
            var unspecifiedDateKind = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);

            var offset = new DateTimeOffset(unspecifiedDateKind);

            return offset.ToUnixTimeMilliseconds();
        }

        public static JobDTO MapJobResponseToDTO(JobResponse response)
        {
            return new JobDTO()
            {
                Id = response.Id,
                IdNumber = response.IdNumber,
                Name = response.Name,
                Status = response.Status,
                StepNumber = response.StepNumber,
                VendorId = response.VendorId.ToString(),
                VendorPriceProfileId = response.VendorPriceProfileId.ToString(),
                StepTypeId = response.StepType.Id,
                StepTypeName = response.StepType.Name,
                JobTypeId = response.StepType.JobTypeId.ToString(),
                Languages = response.Languages
            };;
        }
    }
}
