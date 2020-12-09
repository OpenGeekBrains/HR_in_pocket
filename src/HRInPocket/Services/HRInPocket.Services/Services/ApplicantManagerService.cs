using System;
using System.Collections.Generic;
using System.Linq;

namespace HRInPocket.Services.Services
{
    public class ApplicantManagerService
    {
        private readonly AuthService _authService;

        public ApplicantManagerService(AuthService authService) => _authService = authService;

        private static readonly List<Applicant> Applicants = new List<Applicant>();
        
        
        
        public IEnumerable<Applicant> Get() => Applicants;

        public void Register(Applicant applicant)
        {
            _authService.Register(applicant.UserData);
            Applicants.Add(applicant);
        }

        public bool IsApplicantExist(Guid id)
        {
            if (id == Guid.Empty) return false;
            if (Applicants.FirstOrDefault(a => a.Id == id) is null) return false;
            
            return true;
        }
    }
}