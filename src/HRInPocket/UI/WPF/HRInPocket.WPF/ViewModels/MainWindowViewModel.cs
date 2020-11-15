using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

using HRInPocket.WPF.Infrastructure.Commands;
using HRInPocket.WPF.Services.Interfaces;
using HRInPocket.WPF.ViewModels.Core;

namespace HRInPocket.WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModelCore
    {
        public MainWindowViewModel(ISaveDataToJSON SaveDataToJSON)
        {
            _SaveDataToJSON = SaveDataToJSON;

            SearchCommand = new LambdaCommand(OnSearchCommandExecuted, CanSearchCommandExecute);
            SaveDataToJSONCommand = new LambdaCommand(OnSaveDataToJSONCommandExecuted, CanSaveDataToJSONCommandExecute);
        }

        #region Сервисы

        #region Сервис сохранения данных
        private readonly ISaveDataToJSON _SaveDataToJSON;
        #endregion

        #endregion

        #region Свойства

        #region Title : string - Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _Title = "Главное окно";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Sites : List<string> - Сайты поиска работы

        /// <summary>Сайты поиска работы</summary>
        private List<string> _Sites = new List<string>()
        {
            "hh.ru",
            "superjob.ru"
        };

        /// <summary>Сайты поиска работы</summary>
        public List<string> Sites => _Sites;

        #endregion

        #region KeyWords : string - Ключевые слова для поиска

        /// <summary>Ключевые слова для поиска</summary>
        private string _KeyWords;

        /// <summary>Ключевые слова для поиска</summary>
        public string KeyWords
        {
            get => _KeyWords;
            set => Set(ref _KeyWords, value);
        }

        #endregion

        #region SelectedSite : string - Выбранный в комбобоксе сайт

        /// <summary>Выбранный в комбобоксе сайт</summary>
        private string _SelectedSite;

        /// <summary>Выбранный в комбобоксе сайт</summary>
        public string SelectedSite
        {
            get => _SelectedSite;
            set => Set(ref _SelectedSite, value);
        }

        #endregion

        #region DataCollection : ObservableCollection<Vacancy> - Список данных

        /// <summary>Список данных</summary>
        private ObservableCollection<Vacancy> _DataCollection;

        /// <summary>Список данных</summary>
        public ObservableCollection<Vacancy> DataCollection
        {
            get => _DataCollection;
            set => Set(ref _DataCollection, value);
        }

        #endregion

        #region Status : string - Состояние приложения

        /// <summary>Состояние приложения</summary>
        private string _Status = "Готово";

        /// <summary>Состояние приложения</summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        #endregion

        #endregion

        #region Команды

        #region Поиск на выбранном сайте по ключевому слову
        /// <summary>Поиск на выбранном сайте по ключевому слову</summary>
        public ICommand SearchCommand { get; }
        /// <summary>Поиск на выбранном сайте по ключевому слову</summary>
        private void OnSearchCommandExecuted(object parameter)
        {
            throw new NotImplementedException();
        }

        private bool CanSearchCommandExecute(object parameter) => true;

        #endregion

        #region Сохранение данных в json
        /// <summary>Сохранение данных в json</summary>
        public ICommand SaveDataToJSONCommand { get; }
        /// <summary>Сохранение данных в json</summary>
        private void OnSaveDataToJSONCommandExecuted(object parameter)
        {
            _SaveDataToJSON.SaveDataToFile(DataCollection, SelectedSite);
        }

        private bool CanSaveDataToJSONCommandExecute(object parameter) => true;

        #endregion

        #endregion
    }
}
