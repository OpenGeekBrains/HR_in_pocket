using HRInPocket.Infrastructure.Models.Extensions;

using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Infrastructure.Models
{
    [ModelBinder(typeof(AssignmentTypeModelBinder))]
    public enum AssignmentType { Invitation, Resume, Covering, Feedback }
}