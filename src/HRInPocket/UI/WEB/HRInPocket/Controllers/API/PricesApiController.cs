using System.Collections.Generic;

using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Controllers.API
{
    [Route(WebAPI.Prices)]
    [ApiController]
    public class PricesApiController : ControllerBase
    {
        public ActionResult<IEnumerable<PriceItem>> Get() => new PriceItem[]
        {
            new() { Name = "Составление резюме", Price = 2000 },
            new() { Name = "Написание сопроводительного письма", Price = 800 },
            new() { Name = "Перевод резюме", Price = 1500 },
        };
    }
}
