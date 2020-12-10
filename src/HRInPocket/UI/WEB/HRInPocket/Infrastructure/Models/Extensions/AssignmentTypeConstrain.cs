using System;
using System.Globalization;

using HRInPocket.Domain;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace HRInPocket.Infrastructure.Models.Extensions
{
    // Кастомное ограничение марщрута по типу AssignmentType, для проверки является ли значение в маршруте нужным нам значением
    public class AssignmentTypeConstrain : IRouteConstraint
    {

        #region Implementation of IRouteConstraint

#nullable enable
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            _ = routeKey ?? throw new ArgumentNullException(nameof(routeKey));
            _ = values ?? throw new ArgumentNullException(nameof(values));

            // получаем значение в маршруте
            if (!values.TryGetValue(routeKey, out var value)) return false;

            // парсим в строку игнорируя культуру
            var parameter = Convert.ToString(value, CultureInfo.InvariantCulture);

            // если наш тип возвращаем true, если нет 'идите лесом'
            return Enum.TryParse(typeof(AssignmentType), parameter, out _);
        }
#nullable restore

        #endregion
    }
}