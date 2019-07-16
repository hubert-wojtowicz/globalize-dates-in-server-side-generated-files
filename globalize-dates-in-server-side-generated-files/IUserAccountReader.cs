using globalize_dates_in_server_side_generated_files.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace globalize_dates_in_server_side_generated_files
{
    public interface IUserAccountReader
    {
        Task<IReadOnlyCollection<BalanceItemModel>> GetUserAccountBalance(GetUserBalanceInputModel parameters);
    }
}