using System.Collections.Generic;

using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Controllers.API
{
    [Route(WebAPI.Tarifs)]
    [ApiController]
    public class TarifsApiController : ControllerBase
    {
        public ActionResult<IEnumerable<Tarif>> Get() => new Tarif[]
        {
            new()
            {
                Name = "Базовый",
                Description = "Хороший повод начать",
                Visits = 1,
                Price = 1500
            },
            new()
            {
                Name = "Средний",
                Description = "Набирайте обороты",
                Visits = 3,
                Price = 4500
            },
            new()
            {
                Name = "Эффективный",
                Description = "Выбирайте и сравнивайте разные предложения",
                Visits = 5,
                Price = 6000
            },
            new()
            {
                Name = "Профи",
                Description = "Обеспечите гарантию трудоустройства",
                Visits = 10,
                Price = 10000
            },
        };
    }
}
