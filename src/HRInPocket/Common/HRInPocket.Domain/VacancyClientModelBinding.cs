using System;

using System.Web.Mvc;
using HRInPocket.Domain.Entities.Data;

namespace HRInPocket.Domain
{
    public class VacancyClientModelBinding : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Получаем поставщик значений
            var valueProvider = bindingContext.ValueProvider;

            // получаем данные по одному полю
            var vprId = valueProvider.GetValue("Id");

            // получаем данные по остальным полям
            var id = (Guid)valueProvider.GetValue("Id").ConvertTo(typeof(Guid));
            var minsalary = (int)valueProvider.GetValue("minsalary").ConvertTo(typeof(int));
            var vacancy = new Vacancy() { Id = id, MinSalary = minsalary };

            // если поле Id определено (редактирование)
            if (vprId != null)
            {
                vacancy.Id = (Guid)vprId.ConvertTo(typeof(Guid));
                vacancy.MinSalary = minsalary;
                
            }
            return vacancy;
        }
    }
}