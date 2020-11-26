using System;

namespace HRInPocket.Parsing.hh.ru.Service
{
    class ParseHHVacancyException : Exception
    {
        public ParseHHVacancyException(string message) : base(message) { }

        public ParseHHVacancyException(string message, Exception inner) : base(message, inner) { }
    }
}
