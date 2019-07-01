using Helsi.DataAccess.Models;
using Helsi.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helsi.DomainLogic.Services
{
    public interface IPatientService : IBaseService<Patient>
    {
        IEnumerable<PatientDto> GetPatients();
        PatientDto GetPatient(int id);
        int CreatePatient(PatientDto patientDto);
        void UpdatePatient(PatientDto patientDto);
        void Deactivate(int id);
    }
}
