﻿using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.AnalyzeLoanOpportunities {
    public class CreateLoanApplicationDTO {

        [Required(ErrorMessage = "El campo monto es obligatorio.")]
        public decimal? Amount { get; set; }

        [Required(ErrorMessage = "El campo justificación es obligatorio.")]
        [StringLength(25, ErrorMessage = "El campo justificación no puede tener más de 200 caracteres.")]
        public string Justification { get; set; } = null!;

        [Required(ErrorMessage = "El campo tipo de moneda es obligatorio.")]
        public byte? TypeCurrencyId { get; set; }

        [Required(ErrorMessage = "Debe de subir un archivo válido.")]
        public required FileUploadDTO EmployerOrder { get; set; }

        [Required(ErrorMessage = "Debe de subir un archivo válido.")]
        public required FileUploadDTO SalaryStatement { get; set; }

        [Required(ErrorMessage = "Debe de buscar un cliente")]
        public string AccountId { get; set; } = null!;

        [Required(ErrorMessage = "El campo plazo es obligatorio.")]
        public byte? DeadlineId { get; set; }

        [Required(ErrorMessage = "El campo tipo de prestamo es obligatorio.")]
        public byte? TypeLoanId { get; set; }

        [Required(ErrorMessage = "El campo interes es obligatorio.")]
        public byte? InterestId { get; set; }
        public int UserId { get; set; }
    }
}