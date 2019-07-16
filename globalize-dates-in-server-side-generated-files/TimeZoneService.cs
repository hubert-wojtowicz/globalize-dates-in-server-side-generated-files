using Microsoft.AspNetCore.Http;
using System;
using TimeZoneConverter;

namespace globalize_dates_in_server_side_generated_files
{
    public class TimeZoneService : ITimeZoneService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TimeZoneService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            IanaRequestTimeZoneName = _httpContextAccessor.HttpContext.Request.Headers["Accept-Timezone"];
            RequestTimeZoneInfo = TimeZoneInfoFactory(IanaRequestTimeZoneName);
        }

        public TimeZoneInfo RequestTimeZoneInfo { get; }

        public string IanaRequestTimeZoneName { get; }

        public DateTime ToLocalDate(DateTime dateToConvert)
            => dateToConvert.Kind == DateTimeKind.Utc ? TimeZoneInfo.ConvertTimeFromUtc(dateToConvert, RequestTimeZoneInfo) : dateToConvert;

        private TimeZoneInfo TimeZoneInfoFactory(string ianaTimeZoneName)
        {
            if (string.IsNullOrWhiteSpace(ianaTimeZoneName))
                return TimeZoneInfo.Local;
            var microsoftTimeZoneId = TZConvert.IanaToWindows(ianaTimeZoneName);
            return TimeZoneInfo.FindSystemTimeZoneById(microsoftTimeZoneId);
        }
    }
}
