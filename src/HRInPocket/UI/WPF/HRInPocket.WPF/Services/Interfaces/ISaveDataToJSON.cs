using System.Collections.Generic;

namespace HRInPocket.WPF.Services.Interfaces
{
    interface ISaveDataToJSON
    {
        bool SaveDataToFile<T>(IEnumerable<T> data, string fileName);
    }
}
