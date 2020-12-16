using System;
using System.Threading.Tasks;

using HRInPocket.Domain;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HRInPocket.Infrastructure.Models.Extensions
{
    // тип конвертации модели, альтернатива TypeConverter, с большим контроллем на контекстом
    public class AssignmentTypeModelBinder : IModelBinder
    {
        #region Implementation of IModelBinder

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            _ = bindingContext ?? throw new ArgumentNullException(nameof(bindingContext));

            // проверка на привязку к нужному типу, для уникальности конвёртера можно вынести в конструктор
            if (bindingContext.ModelType == typeof(AssignmentType))
            {
                // получаем значение по ключу
                var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

                // проверяем найдено ли значение по ключу
                if (value != ValueProviderResult.None && Enum.TryParse(typeof(AssignmentType), value.FirstValue, out var result))
                {
                    // возвращаем сконвертированное значение
                    bindingContext.Result = ModelBindingResult.Success(result);
                    // завершаем задачу
                    return Task.CompletedTask;
                }
            }

            // сообщаем о неудаче конвертации
            bindingContext.Result = ModelBindingResult.Failed();
            // завершаем задачу
            return Task.CompletedTask;
        }

        #endregion
    }
}