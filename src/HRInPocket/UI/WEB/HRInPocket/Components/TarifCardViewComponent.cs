using HRInPocket.Domain.Entities.Data;
using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Components
{
    public class TarifCardViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Tarif tarif) => View(tarif);
    }
}
