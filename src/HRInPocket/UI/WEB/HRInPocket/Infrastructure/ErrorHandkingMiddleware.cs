using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Infrastructure
{
    /// <summary>
    /// Промежуточное ПО для обработки ошибок
    /// </summary>
    public class ErrorHandkingMiddleware
    {
        private readonly ILogger<ErrorHandkingMiddleware> _Logger;
        private readonly RequestDelegate _Next;

        public ErrorHandkingMiddleware(RequestDelegate next, ILogger<ErrorHandkingMiddleware> logger)
        {
            _Next = next;
            _Logger = logger;
        }

        /// <summary>
        /// Событие передачи контекста
        /// </summary>
        /// <param name="context">Http Контекст</param>

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _Next(context);
            }
            catch (Exception e)
            {
                HandleException(context, e);
                throw;
            }
        }

        /// <summary>
        /// Логирование ошибки
        /// </summary>
        /// <param name="context">Http Контекст</param>
        /// <param name="error">Исключение</param>

        public void HandleException(HttpContext context, Exception error) =>
            _Logger.LogInformation($"{error.Message}", context.Request.Path);
    }
}
