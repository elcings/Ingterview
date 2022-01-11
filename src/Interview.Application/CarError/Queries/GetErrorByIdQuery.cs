using Interview.Application.Common.Interfaces;
using Interview.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.CarError.Queries
{
    public class GetErrorByIdQuery:IRequest<Response>,ICacheable
    {
        public Guid Id { get; set; }

        public string CacheKey => $"GetErrorById-{Id}";
    }
}
