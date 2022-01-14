using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Common.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        public DateTime ErrorTime { get; set; }
        public string ErrorCause { get; set; }

        protected ExceptionBase()
        {
        }

        protected ExceptionBase(string message, string cause)
            : base(message)
        {
            ErrorTime = DateTime.Now;
            ErrorCause = cause;
        }

        protected ExceptionBase(string message, string cause, DateTime time)
            : base(message)
        {
            ErrorTime = time;
            ErrorCause = cause;
        }

        protected ExceptionBase(string message) : base(message)
        {
        }

        protected ExceptionBase(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExceptionBase(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
