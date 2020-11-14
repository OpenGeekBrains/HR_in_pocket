using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRInPocket.DAL.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HRInPocket.Components
{
    public class TarifCardViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Tarif tarif) => View(tarif);
    }
}
