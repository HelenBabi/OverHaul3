using Overhaul.Application.AutoFac;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.ExceptionHandler
{
    public interface IExceptionHandlerService : IScopedDependency
    {
        Task Handle(HttpContext httpContext, Exception exception);
    }
}
