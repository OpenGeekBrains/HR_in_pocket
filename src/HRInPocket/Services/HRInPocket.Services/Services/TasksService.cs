﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HRInPocket.DAL.Data;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces.Services;

using Microsoft.EntityFrameworkCore;

namespace HRInPocket.Services.Services
{
    public class TasksService : ITasksService
    {
        private readonly ApplicationDbContext _db;

        public TasksService(ApplicationDbContext db) => _db = db;

        public async Task<TargetTask> CreateTask(string UserId, decimal? Salary, string Position, bool RemoteWork, string Tags)
        {
            var profile = await _db.Profiles.FirstOrDefaultAsync(p => p.UserId == UserId);

            var task = new TargetTask
            {
                Salary = Salary ?? 0,
                Tags = Tags,
                RemoteWork = RemoteWork,
                Profile = new Profile { UserId = UserId }
            };

            if (Position != null)
            {
                var speciality = await _db.Specialties.FirstOrDefaultAsync(s => s.Name == Position);

                if (speciality is null)
                {
                    await _db.Specialties.AddAsync(speciality = new Speciality { Name = Position });
                    await _db.SaveChangesAsync();
                }

                task.Speciality = speciality;
            }


            await _db.TargetTasks.AddAsync(task);
            await _db.SaveChangesAsync();

            return task;
        }

        public async Task<IEnumerable<TargetTask>> GetUserTasks(string UserId)
        {
            if (UserId is null)
                return Enumerable.Empty<TargetTask>();

            return await _db.TargetTasks
               .Include(task => task.Profile)
               .Include(task => task.Speciality)
               .Include(task => task.Address)
               .Where(task => task.Profile.UserId == UserId)
               .ToArrayAsync();
        }
    }
}
