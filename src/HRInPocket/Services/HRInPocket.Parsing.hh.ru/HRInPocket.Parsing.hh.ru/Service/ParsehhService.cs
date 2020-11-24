using HRInPocket.Parsing.hh.ru.Interfaces;

namespace HRInPocket.Parsing.hh.ru.Service
{
    ///<inheritdoc cref="IParsehhService"/>
    public class ParsehhService : IParsehhService
    {
        public IParsehh GetPasrse() => new Parsehh();
    }
}

