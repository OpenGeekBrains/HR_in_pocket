using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace HRInPocket.Infrastructure
{
    /// <summary>
    /// Промежуточное ПО для измерения времени обработки запроса
    /// </summary>
    public class TimeLoadMiddleware
    {

        private readonly RequestDelegate _Next;

        public TimeLoadMiddleware(RequestDelegate next)
        {
            _Next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await _Next(context);
            stopwatch.Stop();
            var ts = stopwatch.ElapsedMilliseconds;
            context.Response.Headers.Add("TimeLoad", ts.ToString());

        }

    }
}
