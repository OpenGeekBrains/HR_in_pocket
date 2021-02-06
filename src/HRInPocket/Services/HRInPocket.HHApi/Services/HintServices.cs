using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HRInPocket.HHApi.Interfaces;
using HRInPocket.HHApi.Models.Hints;
using HRInPocket.HHApi.Services.Base;

namespace HRInPocket.HHApi.Services
{
    public class HintServices: BaseClient, IHintServices
    {
        public async Task<Hints<Education>> EducationalHint(string text, SuggestsLocale? locale = null)
        {
            if (text.Length <= 2)
                return default(Hints<Education>);

            var search = $"?text={text}";
            if (locale != null)
                search += $"&locale={locale}";

            var result = await GetJsonAsync<Hints<Education>>(ApiUrl.Suggests.educational + search);

            return result;
        }

        public async Task<Hints<Companies>> CompaniesHint(string text, SuggestsLocale? locale = null)
        {
            if (text.Length <= 2)
                return default(Hints<Companies>);

            var search = $"?text={text}";
            if (locale != null)
                search += $"&locale={locale}";
            
            var result = await GetJsonAsync<Hints<Companies>>(ApiUrl.Suggests.companies + search);

            return result;
        }

        public async Task<Hints<Specialization>> SpecializationHint(string text, SuggestsLocale? locale = null)
        {
            if (text.Length <= 2)
                return default(Hints<Specialization>);

            var search = $"?text={text}";
            if (locale != null)
                search += $"&locale={locale}";

            var result = await GetJsonAsync<Hints<Specialization>>(ApiUrl.Suggests.specialization + search);

            return result;
        }

        public async Task<Hints<Skill>> SkillsHint(string text)
        {
            if (text.Length <= 2)
                return default(Hints<Skill>);

            var result = await GetJsonAsync<Hints<Skill>>($"{ApiUrl.Suggests.skills}?text={text}");

            return result;
        }

        public async Task<Hints<Position>> PositionHint(string text)
        {
            if (text.Length <= 2)
                return default(Hints<Position>);

            var result = await GetJsonAsync<Hints<Position>>($"{ApiUrl.Suggests.positions}?text={text}");

            return result;
        }

        public async Task<Hints<AreaHint>> AreasHint(string text, SuggestsLocale? locale = null)
        {
            if (text.Length <= 2)
                return default(Hints<AreaHint>);

            var search = $"?text={text}";
            if (locale != null)
                search += $"&locale={locale}";

            var result = await GetJsonAsync<Hints<AreaHint>>(ApiUrl.Suggests.areas + search);

            return result;
        }

        public async Task<Hints<AreaHint>> AreasLeavesHint(string text, SuggestsLocale? locale = null)
        {
            if (text.Length <= 2)
                return default(Hints<AreaHint>);

            var search = $"?text={text}";
            if (locale != null)
                search += $"&locale={locale}";

            var result = await GetJsonAsync<Hints<AreaHint>>(ApiUrl.Suggests.areasLeaves + search);

            return result;
        }

        public async Task<Hints<Vacancy>> VacancyHint(string text)
        {
            if (text.Length <= 2)
                return default(Hints<Vacancy>);

            var result = await GetJsonAsync<Hints<Vacancy>>($"{ApiUrl.Suggests.vacancy}?text={text}");

            return result;
        }
    }
}
