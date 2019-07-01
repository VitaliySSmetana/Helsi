using AutoMapper;
using AutoMapper.QueryableExtensions;
using Helsi.DataAccess;
using Helsi.DataAccess.Models;
using Helsi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helsi.DomainLogic.Services
{
    public class AdditionalContactService : BaseService<AdditionalContact>, IAdditionalContactService
    {
        private readonly IMapper _mapper;

        public AdditionalContactService(HelsiContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public IEnumerable<AdditionalContactDto> GetPatientsAdditionalContacts(int patientId)
        {
            var additionalContacts = GetAll()
                .Where(x => x.PatientId == patientId)
                .ProjectTo<AdditionalContactDto>(_mapper.ConfigurationProvider)
                .ToList();

            return additionalContacts;
        }
    }
}
