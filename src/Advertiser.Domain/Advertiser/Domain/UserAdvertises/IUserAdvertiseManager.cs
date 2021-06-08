using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertiser.Domain.UserAdvertises
{
    public interface IUserAdvertiseManager
    {
        Task<List<int>> FindAdvertiseIdsAsync(DateTime time, string status, int limit);

        Task UpdateAdvertiseToExpiredAsync(params int[] ids);

        Task RemoveExpiredAdvertiseOlderThanAsync(int days);
    }
}
