using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Common.Models
{
    public class Response:CQRSResponse
    {
        public object Data { get; set; }
    }
}
