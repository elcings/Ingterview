using FluentValidation;
using Interview.Application.Common.Models;
using Interview.Application.Validations;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interview.Application.Common.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
        where TResponse : CQRSResponse,new()


    {
        private ILogger<TRequest> _logger;
        private IValidationHandler<TRequest> _validationHandler;

        public ValidationBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }
        public ValidationBehavior(ILogger<TRequest> logger, IValidationHandler<TRequest> validationHandler)
        {
            _logger = logger;
            _validationHandler = validationHandler;
        }

        public async  Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType();
            if (_validationHandler == null)
            {
                _logger.LogInformation("{Request} does not have validation handler configured", requestName);
                return await next();
            }

            var result = await _validationHandler.Validate(request);
            if (!result.IsSuccess)
            {
                _logger.LogError("Validation failed for {Request}.Error{error}",requestName,result.ErrorMessage);
                return new TResponse() { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = result.ErrorMessage };
            }
            return await next();
        }
    }
}
