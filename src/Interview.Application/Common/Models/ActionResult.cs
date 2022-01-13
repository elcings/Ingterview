using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Common.Models
{
    public class ActionResult
    {
        public bool IsSucceed { get; private set; }

        public IEnumerable<string> FailureResult { get; private set; }

        public Exception Exception { get; private set; }

        public string ExceptionMessage
        {
            get
            {
                if (Exception != null)
                    return Exception.Message;
                else if (FailureResult?.Count() > 0)
                    return string.Join(Environment.NewLine, FailureResult);
                else
                    return String.Empty;
            }
        }

        public  ActionResult Failure(params string[] failureResult)
        {
            var actionResult = new ActionResult();

            Failure(actionResult, failureResult);

            return actionResult;
        }


        public  ActionResult Failure(Exception exception)
        {
            var actionResult = new ActionResult();

            Failure(actionResult, exception);

            return actionResult;
        }

        public  ActionResult Succeed()
        {
            var result = new ActionResult();

            Succeed(result);

            return result;
        }

        protected  void Succeed(ActionResult result)
        {
            result.IsSucceed = true;
            result.FailureResult = new string[0];
        }

        protected  void Failure(ActionResult result, params string[] failureResult)
        {
            Contract.Requires(failureResult != null);
            Contract.Requires(failureResult.Any());

            result.IsSucceed = false;
            result.FailureResult = failureResult;
        }


        protected  void Failure(ActionResult result, Exception exception)
        {
            Contract.Requires(exception != null);
            result.IsSucceed = false;

            result.Exception = exception;

            var errorMessages = new List<string>();

            while (exception != null)
            {
                errorMessages.Add(exception.Message);
                exception = exception.InnerException;
            }

            result.FailureResult = errorMessages.ToArray();
        }
    }

    public class ActionResult<T> : ActionResult
    {
        public T Data { get; set; }

        public new  ActionResult<T> Failure(params string[] failureResult)
        {
            var result = new ActionResult<T>();
            Failure(result, failureResult);
            return result;
        }

        public new  ActionResult<T> Failure(Exception exception)
        {
            var result = new ActionResult<T>();
            Failure(result, exception);
            return result;
        }

        public  ActionResult<T> Succeed(T data)
        {
            var result = new ActionResult<T>() { Data = data };
            Succeed(result);
            return result;
        }
    }
}
