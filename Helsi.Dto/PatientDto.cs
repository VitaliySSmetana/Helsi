﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Helsi.Dto
{
    public class PatientDto
    {
        public int Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Patronymic { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public int GenderId { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public GenderDto Gender { get; set; }
        public ICollection<AdditionalContactDto> AdditionalContacts { get; set; }
    }
}
