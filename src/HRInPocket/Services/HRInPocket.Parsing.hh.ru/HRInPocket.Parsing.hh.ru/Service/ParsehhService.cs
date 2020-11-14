using System;
using System.Collections.Generic;
using System.Text;

using HRInPocket.Parsing.hh.ru.Interfaces;
using HRInPocket.Parsing.hh.ru.Models.Entites;

namespace HRInPocket.Parsing.hh.ru.Service
{
    public class ParsehhService : IParsehhService
    {
        public IParsehh GetPasrse()
        {
            return new Parsehh();
        }
    }
    public class Parsehh : IParsehh
    {
        public event Action<Vacancy> Result;
        public void Parse(string GetParameters)
        {
            throw new NotImplementedException();
        }
    }
}
