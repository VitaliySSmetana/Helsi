﻿using AutoMapper;
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

        public void CreatePatient(PatientDto patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);

            Create(patient, true);
        }

        public void Deactivate(int id)
        {
            var patient = Get(id);

            if (patient != null)
            {
                patient.IsActive = false;
            }

            Commit();
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
    }
}
