using System;
using System.Threading;
using System.Threading.Tasks;

using HRInPocket.Parsing.hh.ru.Service;

namespace HRInPocket.Parsing.hh.ru.Interfaces
{
    public interface IParsehh
    {
        event EventHandler<VacancyEventArgs> Result;

        Task ParseAsync(CancellationToken token, string GetParameters = null);
    }
}
