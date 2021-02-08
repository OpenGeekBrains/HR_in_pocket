namespace HRInPocket.HHApi
{
    internal static class ApiUrl
    {
        /// <summary>
        /// Справочник всех областей и стран
        /// </summary>
        internal class Areas
        {

            internal const string areas = "/areas";
            internal const string countries = "/areas/countries";

        };

        /// <summary>
        /// Подсказки при поиске и заполнении полей
        /// </summary>
        internal class Suggests
        {
            private const string suggests = "/suggests";

            /// <summary>
            /// Подсказки по названиям университетов
            /// </summary>
            internal const string educational = suggests + "/educational_institutions";

            /// <summary>
            /// Подсказки по организациям
            /// </summary>
            internal const string companies = suggests + "/companies";

            /// <summary>
            /// Подсказки по специализациям
            /// </summary>
            internal const string specialization = suggests + "/fields_of_study";

            /// <summary>
            /// Подсказки по ключевым навыкам
            /// </summary>
            internal const string skills = suggests + "/skill_set";

            /// <summary>
            /// Подсказки по должностям
            /// </summary>
            internal const string positions = suggests + "/positions";

            /// <summary>
            /// Подсказка по всем регионам
            /// </summary>
            internal const string areas = suggests + "/areas";

            /// <summary>
            /// Подсказка по всем регионам, являющимися листами в дереве регионов.
            /// </summary>
            internal const string areasLeaves = suggests + "/area_leaves";

            /// <summary>
            /// Подсказки по ключевым словам поиска вакансий
            /// </summary>
            internal const string vacancy = suggests + "/vacancy_search_keyword";
        }

        /// <summary>
        /// Путь к Api поиска компаний
        /// </summary>
        internal const string employers = "/employers";

        /// <summary>
        /// Cправочник всех отраслей
        /// </summary>
        internal const string industries = "/industries";
    }
}
