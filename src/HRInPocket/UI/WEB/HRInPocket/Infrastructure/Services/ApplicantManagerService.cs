using System;
using System.Collections.Generic;

using HRInPocket.Infrastructure.Models.Records;

namespace HRInPocket.Infrastructure.Services
{
    public class ApplicantManagerService
    {
        private readonly AuthService _authService;

        public ApplicantManagerService(AuthService authService) => _authService = authService;

        private static readonly List<Applicant> Applicants = new();
        
        
        
        public IEnumerable<Applicant> Get() => Applicants;

        public void Register(Applicant applicant)
        {
            _authService.Register(applicant.UserData);
            Applicants.Add(applicant);
        }

        public bool IsApplicantExist(Guid id)
        {
            if (id == Guid.Empty) return false;
            // todo: check if applicant not exist
            return true;
        }
    }
}