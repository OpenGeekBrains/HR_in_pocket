using System;
using System.Collections.Generic;
using System.Text;

using HRInPocket.Parsing.hh.ru.Models.Entites;
using HRInPocket.Parsing.hh.ru.Service;

namespace HRInPocket.Parsing.hh.ru.Interfaces
{
    public interface IParsehhService
    {
        IParsehh GetPasrse();
    }
    public interface IParsehh
    {
        event EventHandler<VacancyEventArgs> Result;

        void Parse(string GetParameters = null);
    }
}
