using System.Threading.Tasks;

using HRInPocket.DAL.Data;
using HRInPocket.Domain.Entities.Users;
using HRInPocket.Interfaces.Services;
using HRInPocket.ViewModels.MakeTask;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly ApplicationDbContext _db;
        private readonly ILogger<TasksController> _Logger;
        private readonly ITasksService _TasksService;

        public TasksController(UserManager<User> UserManager, ApplicationDbContext db, ILogger<TasksController> Logger, ITasksService TasksService)
        {
            _UserManager = UserManager;
            _db = db;
            _Logger = Logger;
            _TasksService = TasksService;
        }

        [HttpGet]
        public IActionResult Create(ShortFormCreateTaskViewModel model = null) => View(model is null 
            ? new CreateTaskViewModel() 
            : new CreateTaskViewModel{ Position = model.Position, ResumeUrl = model.ResumeUrl });

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskViewModel model)
        {
            //if (!model.PermissionToProcessPersonalData)
            //    ModelState.AddModelError(
            //        nameof(CreateTaskViewModel.PermissionToProcessPersonalData),
            //        "Требуется выдать разрешение на обработку персональных данных");

            if (!ModelState.IsValid) return View(model);

            var current_user = await _UserManager.FindByNameAsync(User.Identity.Name);

            //var profile = await _db.Profiles.FirstOrDefaultAsync(p => p.UserId == current_user.Id);

            //var task = new TargetTask
            //{
            //    Salary = model.Salary ?? 0,
            //    Tags = model.Tags,
            //    RemoteWork = model.RemoteWork,
            //    Profile = current_user.Profile ??= new ()
            //};

            //if (model.Position != null)
            //{
            //    var speciality = await _db.Specialties.FirstOrDefaultAsync(s => s.Name == model.Position);

            //    if (speciality is null)
            //    {
            //        await _db.Specialties.AddAsync(speciality = new Speciality { Name = model.Position });
            //        await _db.SaveChangesAsync();
            //    }

            //    task.Speciality = speciality;
            //}


            //await _db.TargetTasks.AddAsync(task);
            //await _db.SaveChangesAsync();

            _ = await _TasksService.CreateTask(current_user.Id, model.Salary, model.Position, model.RemoteWork, model.Tags);

            return RedirectToAction("Index", "Home");
        }
    }
}
