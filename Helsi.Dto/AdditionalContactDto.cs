using System;
using System.Collections.Generic;
using System.Text;

namespace Helsi.Dto
{
    public class AdditionalContactDto
    {
        public int Id { get; set; }
        public int ContactTypeId { get; set; }
        public int PatientId { get; set; }
        public string PhoneNumber { get; set; }

        public ContactTypeDto ContactType { get; set; }
    }
}
