using System.Threading.Tasks;

using HRInPocket.Domain.DTO;

namespace HRInPocket.Interfaces.Clients
{
    public interface IVacancyClient
    {
        Task<bool> CreateRangeAsync(VacancyCollection items);
    }
}