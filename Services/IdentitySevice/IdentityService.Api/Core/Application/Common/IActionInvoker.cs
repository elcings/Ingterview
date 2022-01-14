using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Core.Application.Common
{
    public interface IActionInvoker
    {
        ActionResult<T> Invoke<T>(Func<T> function, string actionName = null, bool useTransaction = false, bool useLog = false, params object[] args);

        ActionResult Invoke(Action action, string actionName = null, bool useTransaction = false, params object[] args);

        Task<ActionResult<T>> InvokeAsync<T>(Func<Task<T>> function, string actionName = null, bool useTransaction = false, bool useLog = false, params object[] args);
        Task<ActionResult> InvokeAsync(Func<Task> action, string actionName = null, bool useTransaction = false, params object[] args);
    }
}
