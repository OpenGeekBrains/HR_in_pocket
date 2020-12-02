using System;

using HRInPocket.Parsing.hh.ru.Models;

namespace HRInPocket.Parsing.hh.ru.Service
{
    public class VacancyEventArgs : EventArgs
    {
        public Vacancy Vacancy { get; set; }
    }
}

