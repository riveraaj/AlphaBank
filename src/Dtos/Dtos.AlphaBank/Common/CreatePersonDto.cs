﻿using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.Common {
    public class CreatePersonDTO {

        [Required(ErrorMessage = "El campo identificación es obligatorio.")]
        public int? PersonId { get; set; }

        [Required(ErrorMessage = "El campo nommbre es obligatorio.")]
        [StringLength(25, ErrorMessage = "El campo nombre no puede tener más de 25 caracteres.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El campo apellido es obligatorio.")]
        [StringLength(25, ErrorMessage = "El campo apellido no puede tener más de 25 caracteres.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "El campo segundo apellido es obligatorio.")]
        [StringLength(25, ErrorMessage = "El campo segundo apellido no puede tener más de 25 caracteres.")]
        public string SecondName { get; set; } = null!;

        [Required(ErrorMessage = "El campo fecha de nacimiento es obligatorio.")]
        [DataType(DataType.Date, ErrorMessage = "El campo fecha de nacimiento  debe ser una fecha válida.")]
        public DateOnly? DateBirth { get; set; }

        [Required(ErrorMessage = "El campo dirección es obligatorio.")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "El campo tipo de identificación es obligatorio.")]
        public byte? TypeIdentificationId { get; set; }

        [Required(ErrorMessage = "El campo nacionalidad es obligatorio.")]
        public byte? NationalityId { get; set; }

        [Required(ErrorMessage = "El campo estado civil es obligatorio.")]
        public byte? MaritalStatusId { get; set; }
    }
}