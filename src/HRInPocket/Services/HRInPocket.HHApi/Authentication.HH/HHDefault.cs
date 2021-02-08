namespace HRInPocket.HHApi.Authentication.HH
{
    public static class HHDefault
    {
        /// <summary>
        /// Схема аутентификации НН. Defaults to <i>HH</i>
        /// </summary>
        public const string AuthenticationScheme = "HH";

        /// <summary>
        /// Название аутентификации HH. Defaults to <i>HH</i>
        /// </summary>
        public static readonly string DisplayName = "HH";

        /// <summary>
        /// Адрес запроса авторизации на НН
        /// </summary>
        public static readonly string AuthorizationEndpoint = "https://hh.ru/oauth/authorize?skip_choose_account=true&";//

        /// <summary>
        /// Адрес запроса получения токенов на НН
        /// </summary>
        public static readonly string TokenEndpoint = "https://hh.ru/oauth/token";

        /// <summary>
        /// Адрес получения информации о пользователе
        /// </summary>
        public static readonly string UserInformationEndpoint = "https://api.hh.ru/me";
    }
}
