using System;

namespace globalize_dates_in_server_side_generated_files.Models
{
    public class BalanceItemModel
    {
        public string UserAccount { get; set; }

        public decimal ChangeValue { get; set; }

        public DateTime ChangeDateTime { get; set; }

        public string ChangeSourceOrTarget { get; set; }
    }
}
