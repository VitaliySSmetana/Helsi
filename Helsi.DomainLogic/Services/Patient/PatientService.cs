using AutoMapper;
using AutoMapper.QueryableExtensions;
using Helsi.DataAccess;
using Helsi.DataAccess.Models;
using Helsi.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helsi.DomainLogic.Services
{
    public class PatientService : BaseService<Patient>, IPatientService
    {
        private readonly IMapper _mapper;
        private readonly IAdditionalContactService _additionalContactService;

        public PatientService(HelsiContext context,
            IMapper mapper,
            IAdditionalContactService additionalContactService) : base(context)
        {
            _mapper = mapper;
            _additionalContactService = additionalContactService;
        }

        public int CreatePatient(PatientDto patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            patient.Id = GeneratePatientId();
            patient.IsActive = true;

            Create(patient);

            return patient.Id;
        }

        public void UpdatePatient(PatientDto patientDto)
        {
            var patient = GetAll()
                .Include(x => x.Gender)
                .Include(x => x.AdditionalContacts)
                .ThenInclude(x => x.ContactType)
                .FirstOrDefault(x => x.Id == patientDto.Id);

            if (patient != null)
            {
                _mapper.Map(patientDto, patient);
                _context.Entry(patient).State = EntityState.Modified;

                Commit();
            }
        }

        public void Deactivate(int id)
        {
            var patient = Get(id);

            if (patient != null)
            {
                patient.IsActive = false;

                Commit();
            }
        }

        public PatientDto GetPatient(int id)
        {
            PatientDto patient = null;

            var entity = GetAll()
                .Include(x => x.Gender)
                .Include(x => x.AdditionalContacts)
                .ThenInclude(x => x.ContactType)
                .FirstOrDefault(x => x.Id == id);

            if (entity != null)
            {
                patient = _mapper.Map<PatientDto>(entity);
            }

            return patient;
        }

        public IEnumerable<PatientDto> GetPatients()
        {
            var patients = GetAll()
                .Include(x => x.AdditionalContacts)
                .ProjectTo<PatientDto>(_mapper.ConfigurationProvider)
                .ToList();

            return patients;
        }

        private int GeneratePatientId()
        {
            var id = GetAll()
                .Max(x => x.Id);

            return ++id;
        }
    }
}
