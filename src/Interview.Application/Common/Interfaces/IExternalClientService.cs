using Interview.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Common.Interfaces
{
    public interface IExternalClientService
    {
        Task<DinResponse> GetDinOfficesAsync();
    }
}
