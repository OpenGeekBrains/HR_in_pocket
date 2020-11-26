using HRInPocket.Domain.Entities.Data;
using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Components
{
    public class PriceItemCardViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PriceItem priceItem) => View(priceItem);
    }
}