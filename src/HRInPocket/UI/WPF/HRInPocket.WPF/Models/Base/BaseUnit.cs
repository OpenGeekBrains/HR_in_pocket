using System;
using System.Collections.Generic;
using System.Text;

namespace HRInPocket.WPF.ViewModels
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    class BaseUnit
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ссылка
        /// </summary>
        public string Url { get; set; }
    }
}
