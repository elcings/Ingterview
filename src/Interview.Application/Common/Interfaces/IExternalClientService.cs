using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Common.Interfaces
{
    public interface IExternalClientService
    {
        Task<long> GetTravelDistanceAsync();
        Task<int> GetFuelLevelAsync();
        Task<string> GetErrorAsync();
    }
}
