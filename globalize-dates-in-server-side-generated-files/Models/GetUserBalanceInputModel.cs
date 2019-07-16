using System;

namespace globalize_dates_in_server_side_generated_files.Models
{
    public class GetUserBalanceInputModel
    {
        public string UserFullName { get; set; }

        public DateTime DateTimeFrom { get; set; }

        public DateTime DateTimeTo { get; set; }
    }
}
