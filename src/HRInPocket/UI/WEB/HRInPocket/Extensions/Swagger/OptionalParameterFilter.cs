using System.Linq;
using System.Text.RegularExpressions;

using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace HRInPocket.Extensions.Swagger
{
    /// <summary>
    /// Фильтр для добавления свагеру функциональности по прочтению опциональности в маршрутах
    /// todo: исправить работу фильтра: находит один параметр и делает его опциональным
    /// </summary>
    public class OptionalParameterFilter : IOperationFilter
    {
        const string captureName = "routeParameter";

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var httpMethodAttributes = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<HttpMethodAttribute>();

            var httpMethodWithOptional = httpMethodAttributes.FirstOrDefault(m => m.Template.Contains("?"));
            if (httpMethodWithOptional == null)
                return;

            var regex = $"{{(?<{captureName}>\\w+)\\?}}";

            var matches = Regex.Matches(httpMethodWithOptional.Template, regex);

            foreach (Match match in matches)
            {
                var name = match.Groups[captureName].Value;

                var parameter = operation.Parameters.FirstOrDefault(p => p.In == ParameterLocation.Path && p.Name == name);
                
                if (parameter == null) continue;
                
                parameter.AllowEmptyValue = true;
                parameter.Description = "Must check \"Send empty value\" or Swagger passes a comma for empty values otherwise";
                parameter.Required = false;
                //parameter.Schema.Default = new OpenApiString(string.Empty);
                parameter.Schema.Nullable = true;
            }
        }
    }
}