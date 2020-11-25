using System;
using System.Linq;
using System.Threading.Tasks;

using HRInPocket.Domain.Entities.Users;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HRInPocket.DAL.Data
{
    public class TestDbInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly ILogger<TestDbInitializer> _logger;

        public TestDbInitializer(
            ApplicationDbContext dbContext,
            UserManager<User> UserManager,
            RoleManager<IdentityRole> RoleManager,
            ILogger<TestDbInitializer> logger)
        {
            _dbContext = dbContext;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
            _logger = logger;
        }

        public void Initialize()
        {
            var db = _dbContext.Database;

            try
            {
                db.Migrate();

                _dbContext
                   .InitTable(TestData.Addresses)
                   .InitTable(TestData.Specialties)
                   .InitTable(TestData.ActivityCategories)
                   .InitTable(TestData.TargetTasks)

                   .InitTable(TestData.Tarifs)
                   .InitTable(TestData.Price)

                ////.InitTable(TestData.Vacancies)           // Not Filled
                ////.InitTable(TestData.CoverLetters)        // Not Filled
                ////.InitTable(TestData.Companies)           // Not Filled
                ////.InitTable(TestData.Profiles)            // Not Filled

                ////.InitTable(TestData.Users)                // Rework for Identity integration
                ////.InitTable(TestData.Applicants)           // Rework for Identity integration
                ////.InitTable(TestData.SystemManagers)       // Rework for Identity integration

                ////.InitTable(TestData.Resumes)              // Behave on Identity Entities
                ;

                InitializeIdentityAsync().Wait();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, "Ошибка инициализации БД");
                throw;
            }
        }

        private async Task InitializeIdentityAsync()
        {
            var admin_role = await _RoleManager.FindByNameAsync(User.AdministratorRole);
            if (admin_role is null)
                await _RoleManager.CreateAsync(admin_role = new IdentityRole { Name = User.AdministratorRole });

            var admin = await _UserManager.FindByNameAsync(User.Administrator);
            if (admin is null)
            {
                admin = new User { UserName = User.Administrator };
                var create_result = await _UserManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (!create_result.Succeeded)
                {
                    var errors = string.Join(",", create_result.Errors.Select(error => error.Description));
                    throw new InvalidOperationException("Не удалось создать администратора в БД:" + errors);
                }

                await _UserManager.AddToRoleAsync(admin, User.AdministratorRole);
            }
        }
    }
}
