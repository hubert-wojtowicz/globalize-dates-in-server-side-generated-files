using System;

namespace globalize_dates_in_server_side_generated_files
{
    public interface ITimeZoneService
    {
        string IanaRequestTimeZoneName { get; }

        TimeZoneInfo RequestTimeZoneInfo { get;  }

        DateTime ToLocalDate(DateTime dateToConvert);
    }
}