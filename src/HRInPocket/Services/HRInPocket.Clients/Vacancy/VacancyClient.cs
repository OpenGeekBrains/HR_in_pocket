using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using HRInPocket.Clients.Base;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Repository.Base;

using Microsoft.Extensions.Configuration;

namespace HRInPocket.Clients.Vacancy
{
    public class VacancyClient : BaseClient, IDataRepository<Domain.Entities.Data.Vacancy>
    {
        public VacancyClient(IConfiguration configuration) : base(configuration, WebAPI.Vacancy) { }

        /// <summary>
        /// запрос всех данных из таблицы
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Domain.Entities.Data.Vacancy>> GetAllAsync() => await Client.GetFromJsonAsync<IEnumerable<Domain.Entities.Data.Vacancy>>(ServiceAddress);

        public IEnumerable<Domain.Entities.Data.Vacancy> GetAll() =>
            Client.GetFromJsonAsync<IEnumerable<Domain.Entities.Data.Vacancy>>(ServiceAddress).Result;

        /// <summary>
        /// запросить все данные из таблицы
        /// </summary>
        /// <returns></returns>
        public IQueryable<Domain.Entities.Data.Vacancy> GetQueryable() => GetAll().AsQueryable();

        public  Task<IQueryable<Domain.Entities.Data.Vacancy>> GetQueryableAsync() => throw new NotImplementedException();

        /// <summary>
        /// запросить данные по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.Data.Vacancy GetById(Guid id)
        {
            var response = Client.GetAsync($"{ServiceAddress}/{id}").Result;
            return response.EnsureSuccessStatusCode().Content.ReadAsAsync<Domain.Entities.Data.Vacancy>().Result;
        }

        public async Task<Domain.Entities.Data.Vacancy> GetByIdAsync(Guid id)
        {
            var response =  await Client.GetAsync($"{ServiceAddress}/{id}");
            return response.EnsureSuccessStatusCode().Content.ReadAsAsync<Domain.Entities.Data.Vacancy>().Result;
        }

        /// <summary>
        /// Создание объекта в базе данных
        /// </summary>
        /// <param name="item"></param>
        public async Task<Guid> CreateAsync(Domain.Entities.Data.Vacancy item)
        {

            var response = await PostAsync(ServiceAddress, item);
            return await response.Content.ReadAsAsync<Guid>();
        }
        public Guid Create(Domain.Entities.Data.Vacancy item)
        {
            var response = PostAsync(ServiceAddress, item).Result;
            return response.Content.ReadAsAsync<Guid>().Result;
        }


        /// <summary>
        /// Создать диапозон объектов
        /// </summary>
        /// <param name="items"></param>
        public void CreateRange(IEnumerable<Domain.Entities.Data.Vacancy> items)
        {
 

        }

        public async Task CreateRangeAsync(IEnumerable<Domain.Entities.Data.Vacancy> items)
        {
           
          
        }

        /// <summary>
        /// редактировать объект в базе данных
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Edit(Domain.Entities.Data.Vacancy item) => Put(ServiceAddress, item).IsSuccessStatusCode;

        public async Task<bool> EditAsync(Domain.Entities.Data.Vacancy item)
        {
            var response = await Client.PutAsJsonAsync(ServiceAddress, item);
            return await response.Content.ReadAsAsync<bool>();
        }

        /// <summary>
        /// Удаление объекта из базы данных
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Remove(Guid id)
        {
            var response = Client.DeleteAsync($"{ServiceAddress}/{id}").Result;
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var response =await Client.DeleteAsync($"{ServiceAddress}/{id}");
            return await response.Content.ReadAsAsync<bool>();
        }

        /// <summary>
        /// Удаление диапозона объектов
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public bool RemoveRange(IEnumerable<Domain.Entities.Data.Vacancy> items)
        {
            var response = Client.DeleteAsync($"{ServiceAddress}/{items}").Result;

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveRangeAsync(IEnumerable<Domain.Entities.Data.Vacancy> items)
        {
            var response = await Client.DeleteAsync($"{ServiceAddress}/{items}");
            return await response.Content.ReadAsAsync<bool>();
        }
    }
}
