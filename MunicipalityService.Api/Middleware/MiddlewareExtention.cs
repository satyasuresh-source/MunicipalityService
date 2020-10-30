using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MunicipalityService.Api.Middleware
{
    public static class MiddlewareExtention
    {
        public static IApplicationBuilder UseErrorHandlingMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
