using Helsi.Dto.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helsi.Dto
{
    public class PatientDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(128, ErrorMessage = "Max length for {0} is 128 characters")]
        [RegularExpression(@"^([А-ЯЁа-яё -]+)$")]
        public string LastName { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "Max length for {0} is 128 characters")]
        [RegularExpression(@"^([А-ЯЁа-яё -]+)$")]
        public string FirstName { get; set; }

        [StringLength(128, ErrorMessage = "Max length for {0} is 128 characters")]
        [RegularExpression(@"^([А-ЯЁа-яё -]+)$")]
        public string Patronymic { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CustomDateRange("1/1/1900")]
        public DateTime BirthDate { get; set; }

        [Required]
        public int GenderId { get; set; }

        [Required]
        [RegularExpression(@"^(\+)(\d{12})$")]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public GenderDto Gender { get; set; }

        public ICollection<AdditionalContactDto> AdditionalContacts { get; set; }
    }
}
