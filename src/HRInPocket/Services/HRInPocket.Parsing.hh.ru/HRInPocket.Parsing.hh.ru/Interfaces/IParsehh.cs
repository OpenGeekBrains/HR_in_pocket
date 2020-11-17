using System;
using System.Collections.Generic;
using System.Text;

using HRInPocket.Parsing.hh.ru.Models.Entites;

namespace HRInPocket.Parsing.hh.ru.Interfaces
{
    public interface IParsehhService
    {
        IParsehh GetPasrse();
    }
    public interface IParsehh
    {
        //event Action<Vacancy> Result;
        event EventHandler<Vacancy> Result;

        void Parse(string GetParameters = null);
    }
}
