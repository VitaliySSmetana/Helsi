using System;
using System.Collections.Generic;

namespace Helsi.Dto
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }
        public string PhoneNumber { get; set; }

        public GenderDto Gender { get; set; }
        public ICollection<AdditionalContactDto> AdditionalContactDtos { get; set; }
    }
}
