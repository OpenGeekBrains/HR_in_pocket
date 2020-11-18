using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HRInPocket.Domain.Entities.Data;
using HRInPocket.ViewModels.MakeTask;

using Profile = AutoMapper.Profile;

namespace HRInPocket.Infrastructure.Profiles
{
    public class TasksProfile : Profile
    {
        public TasksProfile()
        {
            //todo: Настроить маппинг модели
            CreateMap<CreateTaskViewModel, TargetTask>();
        }
    }
}
