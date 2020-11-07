using HRInPocket.WPF.ViewModels.Core;

namespace HRInPocket.WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModelCore
    {
        public MainWindowViewModel()
        {

        }

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

        #endregion
    }
}
