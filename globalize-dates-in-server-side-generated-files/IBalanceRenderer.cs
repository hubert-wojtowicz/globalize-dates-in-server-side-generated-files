using globalize_dates_in_server_side_generated_files.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace globalize_dates_in_server_side_generated_files
{
    public interface IBalanceRenderer
    {
        Task<Stream> RenderBalancePdf(GetUserBalanceInputModel parameters, IReadOnlyCollection<BalanceItemModel> balanceItems);
    }
}