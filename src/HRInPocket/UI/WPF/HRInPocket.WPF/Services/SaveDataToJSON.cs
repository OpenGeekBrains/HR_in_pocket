using System;
using System.Collections.Generic;
using System.IO;

using HRInPocket.WPF.Services.Interfaces;

using Newtonsoft.Json;

namespace HRInPocket.WPF.Services
{
    class SaveDataToJSON : ISaveDataToJSON
    {
        /// <summary>
        /// Сохранение данных в файл
        /// </summary>
        /// <typeparam name="T">Тип сохраняемых данных</typeparam>
        /// <param name="data">Данные, сохраняемые в файл</param>
        /// <param name="fileName">Имя в файла, который будет происходить запись данных</param>
        /// <returns>True, если сохранение прошло успешно</returns>
        public bool SaveDataToFile<T>(IEnumerable<T> data, string fileName)
        {
            if (data == null)
            {
                return false;
            }

            using (StreamWriter file = File.CreateText(fileName + "--" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm") + ".json"))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, data);
            }
            return true;
        }
    }
}
