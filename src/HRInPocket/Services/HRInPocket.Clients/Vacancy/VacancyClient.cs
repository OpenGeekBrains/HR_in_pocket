using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using HRInPocket.Clients.Base;
using HRInPocket.Clients.Service;
using HRInPocket.Domain.DTO;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Clients;
using HRInPocket.Interfaces.Repository.Base;

using Microsoft.Extensions.Configuration;

namespace HRInPocket.Clients.Vacancy
{
    public class VacancyClient : BaseClient, IVacancyClient
    {
        public VacancyClient(HttpClient Client) : base(Client) { }

        protected string ServiceAddress = WebAPI.Vacancy;
        ///// <summary>
        ///// запрос всех данных из таблицы
        ///// </summary>
        ///// <returns></returns>
        //public async Task<IEnumerable<Domain.Entities.Data.Vacancy>> GetAllAsync() => await Client.GetFromJsonAsync<IEnumerable<Domain.Entities.Data.Vacancy>>(ServiceAddress);

        //public IEnumerable<Domain.Entities.Data.Vacancy> GetAll() =>
        //    Client.GetFromJsonAsync<IEnumerable<Domain.Entities.Data.Vacancy>>(ServiceAddress).Result;

        ///// <summary>
        ///// запросить все данные из таблицы
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<Domain.Entities.Data.Vacancy> GetQueryable() => GetAll().AsQueryable();

        //public  Task<IQueryable<Domain.Entities.Data.Vacancy>> GetQueryableAsync() => throw new NotImplementedException();

        ///// <summary>
        ///// запросить данные по идентификатору
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public Domain.Entities.Data.Vacancy GetById(Guid id)
        //{
        //    var response = Client.GetAsync($"{ServiceAddress}/{id}").Result;
        //    return response.EnsureSuccessStatusCode().Content.ReadAsAsync<Domain.Entities.Data.Vacancy>().Result;
        //}

        //public async Task<Domain.Entities.Data.Vacancy> GetByIdAsync(Guid id)
        //{
        //    var response =  await Client.GetAsync($"{ServiceAddress}/{id}");
        //    return response.EnsureSuccessStatusCode().Content.ReadAsAsync<Domain.Entities.Data.Vacancy>().Result;
        //}

        ///// <summary>
        ///// Создание объекта в базе данных
        ///// </summary>
        ///// <param name="item"></param>
        //public async Task<Guid> CreateAsync(Domain.Entities.Data.Vacancy item)
        //{

        //    var response = await Client.PostAsJsonAsync(ServiceAddress, item);
        //    return await response.Content.ReadAsAsync<Guid>();
        //}

        //public Guid Create(Domain.Entities.Data.Vacancy item)
        //{
        //    var response = Client.PostAsJsonAsync(ServiceAddress, item).Result;
        //    return response.Content.ReadAsAsync<Guid>().Result;
        //}



        ///// <summary>
        ///// Создать диапозон объектов
        ///// </summary>
        ///// <param name="items"></param>
        //public void CreateRange(IEnumerable<Domain.Entities.Data.Vacancy> items)=>
        //    PostAsync(ServiceAddress, items).Wait();


        public async Task<bool> CreateRangeAsync(VacancyCollection items) => await PostAsync($"{ServiceAddress}/CreateRange", items).ResultAs<bool>();

        ///// <summary>
        ///// редактировать объект в базе данных
        ///// </summary>
        ///// <param name="item"></param>
        ///// <returns></returns>
        //public bool Edit(Domain.Entities.Data.Vacancy item) => Put(ServiceAddress, item);

        //public async Task<bool> EditAsync(Domain.Entities.Data.Vacancy item) => await PutAsync(ServiceAddress, item);

        ///// <summary>
        ///// Удаление объекта из базы данных
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public bool Remove(Guid id) => Delete(ServiceAddress, id);

        //public async Task<bool> RemoveAsync(Guid id) => await DeleteAsync(ServiceAddress,id);

        ///// <summary>
        ///// Удаление диапозона объектов
        ///// </summary>
        ///// <param name="items"></param>
        ///// <returns></returns>
        //public bool RemoveRange(IEnumerable<Domain.Entities.Data.Vacancy> items) => Delete(ServiceAddress, items);

        //public async Task<bool> RemoveRangeAsync(IEnumerable<Domain.Entities.Data.Vacancy> items)=> 
        //    await DeleteAsync(ServiceAddress,items);
    }
}
