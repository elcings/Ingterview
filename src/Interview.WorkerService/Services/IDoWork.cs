using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.WorkerService.Services
{
    public interface IDoWork
    {
        Task RunAsync();
    }
}
