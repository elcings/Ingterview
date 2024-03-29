﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Validations
{
    public interface IValidationHandler
    {
    }

    public interface IValidationHandler<T>:IValidationHandler
    {
        Task<ValidationResult> Validate(T request);
    }
}
