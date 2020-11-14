using System;
using System.Collections.Generic;
using System.Text;

namespace HRInPocket.Parsing.hh.ru.Interfaces
{
    public interface IParsehhService
    {
        IParsehh GetPasrse();
    }
    public interface IParsehh
    {
        void Parse(string GetParameters);
    }
}
