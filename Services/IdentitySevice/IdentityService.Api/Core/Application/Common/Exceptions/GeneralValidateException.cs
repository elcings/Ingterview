using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Core.Application.Common.Exceptions
{
    public class GeneralValidateException : ExceptionBase
    {
        const string cause = "Validate is invalid";
        const string message = "Validate is invalid!";

        public GeneralValidateException(string message = message) : base(message, cause)
        {
        }

        public GeneralValidateException(string message = message, string cause = cause) : base(message, cause)
        {
        }
    }
}
