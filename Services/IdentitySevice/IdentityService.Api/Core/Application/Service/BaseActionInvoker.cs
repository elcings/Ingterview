using IdentityService.Api.Core.Application.Common;
using IdentityService.Api.Core.Application.Common.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace IdentityService.Api.Core.Application.Service
{
    public class BaseActionInvoker : IActionInvoker
    {
        private ILogger<BaseActionInvoker> _logger;
        public BaseActionInvoker(ILogger<BaseActionInvoker>logger)
        {
            _logger = logger;
        }
        public ActionResult<T> Invoke<T>(Func<T> function, string actionName = null, bool useTransaction = false, bool useLog = false, params object[] args)
        {
            try
            {
                T result;
                _logger.LogInformation(string.Format("{0}. cagrildi", actionName));
                if (useTransaction)
                    result = (T)TransactionInvoker(function);
                else
                    result = (T)function.Invoke();
                if (useLog)
                    _logger.LogInformation(string.Format("{0}.Result:{1}", actionName, JsonConvert.SerializeObject(ActionResult<T>.Succeed(result))));
                return ActionResult<T>.Succeed(result);
            }

            catch (GeneralValidateException exc)
            {
                _logger.LogError(exc, string.Format("{0}.Exception:{1}", actionName, $"{exc.Message} <---->ErrorCause: {exc.ErrorCause}"));
                return ActionResult<T>.Failure(exc.Message);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, string.Format("{0}.Exception:{1}", actionName, exc.Message));
                return ActionResult<T>.Failure("ProblemOccured");
            }
            finally
            {

            }
        }
        public ActionResult Invoke(Action action, string actionName = null, bool useTransaction = false, params object[] args)
        {
            var result = Invoke<object>(() =>
            {
                action.Invoke();
                return null;
            }, actionName, useTransaction);

            if (result.IsSucceed)
                return ActionResult.Succeed();
            else
                return ActionResult.Failure(result.ExceptionMessage);

        }

        public async Task<ActionResult<T>> InvokeAsync<T>(Func<Task<T>> function, string actionName = null, bool useTransaction = false, bool useLog = false, params object[] args)
        {
            try
            {
                T result;
                if (useTransaction)
                    result = (T)await TransactionInvokerAsync(function);
                else
                    result = (T)await function.Invoke();
                if (useLog)
                    _logger.LogInformation(string.Format("{0}.Result:{1}", actionName, JsonConvert.SerializeObject(ActionResult<T>.Succeed(result))));
                return ActionResult<T>.Succeed(result);
            }
            catch (GeneralValidateException exc)
            {
                _logger.LogError(exc, string.Format("{0}.Exception:{1}", actionName, exc.Message));
                return ActionResult<T>.Failure(exc.Message);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, string.Format("{0}.Exception:{1}", actionName, exc.Message));
                return ActionResult<T>.Failure("Problem occured");
            }
            finally
            {

            }
        }

        public async Task<ActionResult> InvokeAsync(Func<Task> action, string actionName = null, bool useTransaction = false, params object[] args)
        {
            var result = await InvokeAsync<object>(async () =>
            {
                await action.Invoke();
                return null;
            }, actionName, useTransaction);
            if (result.IsSucceed)
                return ActionResult.Succeed();
            else
                return ActionResult.Failure(result.ExceptionMessage);
        }

        private T TransactionInvoker<T>(Func<T> function)
        {
            using (var scope = new TransactionScope())
            {

                T result = (T)function.Invoke();

                scope.Complete();

                return result;
            }

        }

        private async Task<T> TransactionInvokerAsync<T>(Func<Task<T>> function)
        {
            using (var scope = new TransactionScope())
            {

                T result = (T)await function.Invoke();

                scope.Complete();

                return result;
            }

        }


    }
}
