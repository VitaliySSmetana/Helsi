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
        void CreatePatient(PatientDto patientDto);
        void Deactivate(int id);
    }
}
