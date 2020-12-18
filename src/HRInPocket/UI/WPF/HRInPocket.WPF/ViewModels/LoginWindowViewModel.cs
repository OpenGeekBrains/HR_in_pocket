using System;
using System.Windows.Controls;
using System.Windows.Input;

using HRInPocket.WPF.Infrastructure.Commands;
using HRInPocket.WPF.ViewModels.Core;

namespace HRInPocket.WPF.ViewModels
{
    class LoginWindowViewModel : ViewModelCore
    {
        public LoginWindowViewModel()
        {
            LoginCommand = new LambdaCommand(OnLoginCommandExecuted, CanLoginCommandExecute);
        }

        #region Свойства

        #region Login : string - Имя пользователя

        /// <summary>Имя пользователя</summary>
        private string _Login;

        /// <summary>Имя пользователя</summary>
        public string Login
        {
            get => _Login;
            set => Set(ref _Login, value);
        }

        #endregion

        #region Message : string - Сообщение о входе или об ошибке входа

        /// <summary>Сообщение о входе или об ошибке входа</summary>
        private string _Message;

        /// <summary>Сообщение о входе или об ошибке входа</summary>
        public string Message
        {
            get => _Message;
            set => Set(ref _Message, value);
        }

        #endregion

        #endregion

        #region Команды

        #region Осуществление входа
        /// <summary>Осуществление входа</summary>
        public ICommand LoginCommand { get; }
        /// <summary>Осуществление входа</summary>
        private void OnLoginCommandExecuted(object parameter)
        {
            if (string.IsNullOrEmpty(Login)) Message = "Введите имя пользователя";
            else if (parameter is PasswordBox passwordBox && string.IsNullOrEmpty(passwordBox.Password)) Message = "Введите пароль";

            //todo: Реализация авторизации
        }

        private bool CanLoginCommandExecute(object parameter) => true;

        #endregion

        #endregion
    }
}
