using HRInPocket.DAL.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Components
{
    public class PriceItemCardViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PriceItem priceItem) => View(priceItem);
    }
}