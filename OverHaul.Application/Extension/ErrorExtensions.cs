using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Overhaul.Application.Extension
{
    public static class ErrorExtensions
    {
        public static string ToStringMessage(this ModelStateDictionary modelState)
        {
          var errors =    modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .Where(msg => !string.IsNullOrWhiteSpace(msg))
                .ToList();
            return string.Join(" | ", errors);
        }
    }
}
