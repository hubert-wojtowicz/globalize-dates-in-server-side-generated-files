using System;

namespace globalize_dates_in_server_side_generated_files.Models
{
    public class GetBalanceRequest
    {
        public Guid UserId { get; set; }

        public DateTime DateTimeFrom { get; set; }

        public DateTime DateTimeTo { get; set; }
    }
}
