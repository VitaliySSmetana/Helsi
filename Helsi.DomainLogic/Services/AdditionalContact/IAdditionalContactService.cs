using Helsi.DataAccess.Models;
using Helsi.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helsi.DomainLogic.Services
{
    public interface IAdditionalContactService : IBaseService<AdditionalContact>
    {
        IEnumerable<AdditionalContactDto> GetPatientsAdditionalContacts(int patientId);
    }
}
