using System;
using System.Collections.Generic;

namespace Helsi.DataAccess.Models
{
    public partial class Patient
    {
        public Patient()
        {
            AdditionalContacts = new HashSet<AdditionalContact>();
        }

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual ICollection<AdditionalContact> AdditionalContacts { get; set; }
    }
}
