using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using HRInPocket.Parsing.hh.ru.Interfaces;
using HRInPocket.Parsing.hh.ru.Models;
using HRInPocket.Parsing.hh.ru.Service;
using HRInPocket.WPF.Data;
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
            _Parsehh = ParsehhService.GetParse();
            _Parsehh.SendVacancy += GetDataCollection;

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
        private readonly List<string> _Sites = new List<string>()
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
        private ObservableCollection<Vacancy> _DataCollection = TestData.GetTestData();

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

        #region ButtonContent : string - Текст кнопки

        /// <summary>Текст кнопки</summary>
        private string _ButtonContent = "Запустить";

        /// <summary>Текст кнопки</summary>
        public string ButtonContent
        {
            get => _ButtonContent;
            set => Set(ref _ButtonContent, value);
        }

        #endregion

        #region StopParse : bool - Остановка парсера

        /// <summary>Остановка парсера</summary>
        private bool _StopParse = false;

        /// <summary>Остановка парсера</summary>
        public bool StopParse
        {
            get => _StopParse;
            set => Set(ref _StopParse, value);
        }

        #endregion

        #region Page : string - Адрес hh.ru

        /// <summary>Адрес hh.ru</summary>
        private string _Page = "https://hh.ru/search/vacancy";

        /// <summary>Адрес hh.ru</summary>
        public string Page
        {
            get => _Page;
            set => Set(ref _Page, value);
        }

        #endregion
        /// <summary>Источник токена отмены асинхронной операции</summary>
        private static CancellationTokenSource s_cts;

        #endregion

        #region Команды

        #region Поиск на выбранном сайте по ключевому слову
        /// <summary>Поиск на выбранном сайте по ключевому слову</summary>
        public ICommand SearchCommand { get; }
        /// <summary>Поиск на выбранном сайте по ключевому слову</summary>
        private void OnSearchCommandExecuted(object parameter)
        {
            if (StopParse)
            {
                s_cts.Cancel();
                StopParse = false;
                Status = $"Парсер остановлен. Получено {DataCollection.Count} вакансий";
                ButtonContent = "Запустить";
                s_cts.Dispose();
            }
            else
            {
                DataCollection = new ObservableCollection<Vacancy>();
                s_cts = new CancellationTokenSource();

                // Метод парсера, который возвращает вакансии через событие:
                //_Parsehh.ParseAsync(s_cts.Token, Page, KeyWords);

                // Метод парсера, который возвращает вакансии через IAsyncEnumerable
                //_ = GetDataCollectionAsync(s_cts.Token);

                /// Метод парсера, который возвращает вакансии через Task<(Vacancy[], string)>
                _ = GetDataArrayAsync(s_cts.Token);

                StopParse = true;
                ButtonContent = "Остановить";
                Status = "Парсер запущен";
            }
        }

        private bool CanSearchCommandExecute(object parameter) => true;

        #endregion

        #region Сохранение данных в json
        /// <summary>Сохранение данных в json</summary>
        public ICommand SaveDataToJSONCommand { get; }
        /// <summary>Сохранение данных в json</summary>
        private void OnSaveDataToJSONCommandExecuted(object parameter) => _SaveDataToJSON.SaveDataToFile(DataCollection, SelectedSite);

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

        /// <summary> Метод получения вакансий из IAsyncEnumerable<Vacancy> </summary>
        /// <param name="token">Токен отмены парсера</param>
        /// <returns></returns>
        private async Task GetDataCollectionAsync(CancellationToken token)
        {
            var data = _Parsehh.ParseEnumerableAsync(token, Page, KeyWords);
            await foreach (var item in data)
            {
                if (item == null) return;
                DataCollection.Add(item);
            }
        }

        private async Task GetDataArrayAsync(CancellationToken token)
        {
            do
            {
                var (Vacancies, NextPage) = await _Parsehh.ParseAsync(token, Page);
                foreach (var item in Vacancies) DataCollection.Add(item);
                Page = NextPage;
            } while (!string.IsNullOrEmpty(Page));
        }
    }
}
