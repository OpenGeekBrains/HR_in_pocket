using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using HRInPocket.Clients.Base;
using HRInPocket.Interfaces;

namespace HRInPocket.Clients.Vacancy
{
    public class VacancyClient : BaseClient, IDataRepository<Domain.Entities.Data.Vacancy>
    {
        public VacancyClient(HttpClient Client) : base(Client) { }
        
        public ICollection<Domain.Entities.Data.Vacancy> GetAll() => 
            Client.GetFromJsonAsync<ICollection<Domain.Entities.Data.Vacancy>>(ApiRoutes.Vacancy).Result;

        public async Task<ICollection<Domain.Entities.Data.Vacancy>> GetAllAsync() => 
            await Client.GetFromJsonAsync<ICollection<Domain.Entities.Data.Vacancy>>(ApiRoutes.Vacancy);

        public IQueryable<Domain.Entities.Data.Vacancy> GetQueryable() => GetAll().AsQueryable();

        public async Task<IQueryable<Domain.Entities.Data.Vacancy>> GetQueryableAsync() => throw new NotImplementedException();

        public Domain.Entities.Data.Vacancy GetById(Guid id) => throw new NotImplementedException();

        public async Task<Domain.Entities.Data.Vacancy> GetByIdAsync(Guid id) => throw new NotImplementedException();

        public Guid Create(Domain.Entities.Data.Vacancy item) => throw new NotImplementedException();

        public async Task<Guid> CreateAsync(Domain.Entities.Data.Vacancy item) => throw new NotImplementedException();

        public bool Edit(Domain.Entities.Data.Vacancy item) => throw new NotImplementedException();

        public async Task<bool> EditAsync(Domain.Entities.Data.Vacancy item) => throw new NotImplementedException();

        public bool Remove(Guid id) => throw new NotImplementedException();

        public async Task<bool> RemoveAsync(Guid id) => throw new NotImplementedException();

        public void CreateRange(ICollection<Domain.Entities.Data.Vacancy> items) => throw new NotImplementedException();

        public async Task CreateRangeAsync(ICollection<Domain.Entities.Data.Vacancy> items) => throw new NotImplementedException();

        public bool RemoveRange(ICollection<Domain.Entities.Data.Vacancy> items) => throw new NotImplementedException();

        public async Task<bool> RemoveRangeAsync(ICollection<Domain.Entities.Data.Vacancy> items) => throw new NotImplementedException();
    }
}
