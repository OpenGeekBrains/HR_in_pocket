using HRInPocket.DAL.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Components
{
    public class TarifCardViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Tarif tarif) => View(tarif);
    }
}
