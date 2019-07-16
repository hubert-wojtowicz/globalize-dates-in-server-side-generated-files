using globalize_dates_in_server_side_generated_files.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace globalize_dates_in_server_side_generated_files
{
    public class UserAccountReader : IUserAccountReader
    {
        public async Task<IReadOnlyCollection<BalanceItemModel>> GetUserAccountBalance(GetUserBalanceInputModel parameters)
        {
            string content = await File.ReadAllTextAsync("transactions.json", Encoding.UTF8);
            var items = JsonConvert.DeserializeObject<BalanceItemModel[]>(content);

            return items
                .Where(x => x.UserAccount == parameters.UserFullName)
                .Where(x => parameters.DateTimeFrom <= x.ChangeDateTime)
                .Where(x => x.ChangeDateTime < parameters.DateTimeTo)
                .ToArray();
        }
    }
}
