using System;

namespace HRInPocket.Infrastructure.Models.Records.Assignments
{
    public record InvitationAssignment(long id, string place_name, Guid applicant_id) : Assignment(id, place_name, applicant_id)
    {

    }
}