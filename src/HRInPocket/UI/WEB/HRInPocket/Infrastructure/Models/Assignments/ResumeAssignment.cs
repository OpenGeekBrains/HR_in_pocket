using System;

namespace HRInPocket.Infrastructure.Models.Assignments
{
    public record ResumeAssignment(long id, string place_name, Guid applicant_id) : Assignment(id, place_name, applicant_id)
    {

    }
}