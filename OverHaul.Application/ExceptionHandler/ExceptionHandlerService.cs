using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.ExceptionHandler
{
    public class ExceptionHandlerService : IExceptionHandlerService
    {
        private readonly IHostEnvironment hostEnvironment;
        public ExceptionHandlerService(IHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }
        public async Task Handle(HttpContext httpContext, Exception exception)
        {
            if (hostEnvironment.IsDevelopment())
            {
                //TODO complete this
                await httpContext.Response.WriteAsync(exception.Message.ToString());
            }
            else
                await httpContext.Response.WriteAsync(exception.Message.ToString());
        }
    }
}
