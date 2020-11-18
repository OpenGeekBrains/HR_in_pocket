using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

using HRInPocket.Parsing.hh.ru.Interfaces;
using HRInPocket.Parsing.hh.ru.Models.Entites;
using HRInPocket.Parsing.hh.ru.Service;
using HRInPocket.WPF.Infrastructure.Commands;
using HRInPocket.WPF.Services.Interfaces;
using HRInPocket.WPF.ViewModels.Core;

namespace HRInPocket.WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModelCore
    {
        public MainWindowViewModel(ISaveDataToJSON SaveDataToJSON,
                                    IParsehhService ParsehhService)
        {
            _SaveDataToJSON = SaveDataToJSON;
            _Parsehh = ParsehhService.GetPasrse();
            _Parsehh.Result += GetDataCollection;

            SearchCommand = new LambdaCommand(OnSearchCommandExecuted, CanSearchCommandExecute);
            SaveDataToJSONCommand = new LambdaCommand(OnSaveDataToJSONCommandExecuted, CanSaveDataToJSONCommandExecute);
        }

        

        /// <summary>Сервис сохранения данных</summary>
        private readonly ISaveDataToJSON _SaveDataToJSON;

        /// <summary>Сервис парсинга hh.ru</summary>
        private readonly IParsehh _Parsehh;

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

        #region SearchSwitcher : bool - Переключатель состояния кнопки поиска

        /// <summary>Переключатель состояния кнопки поиска</summary>
        private bool _SearchSwitcher = true;

        /// <summary>Переключатель состояния кнопки поиска</summary>
        public bool SearchSwitcher
        {
            get => _SearchSwitcher;
            set => Set(ref _SearchSwitcher, value);
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
            _Parsehh.Parse();
            SearchSwitcher = false;
        }

        private bool CanSearchCommandExecute(object parameter) => SearchSwitcher;

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

        #region Методы
        /// <summary>
        /// Добавление в коллекцию полученной вакансии
        /// </summary>
        /// <param name="vacancy">Полученная вакансия</param>
        private void GetDataCollection(object sender, VacancyEventArgs e)
        {
            if (DataCollection == null) DataCollection = new ObservableCollection<Vacancy>();

            DataCollection.Add(e.Vacancy);
        }

        #endregion
    }
}
