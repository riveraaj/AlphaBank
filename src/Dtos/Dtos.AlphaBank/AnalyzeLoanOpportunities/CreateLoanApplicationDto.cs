﻿using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank.AnalyzeLoanOpportunities {
    public class CreateLoanApplicationDto {

        [Required(ErrorMessage = "El campo monto es obligatorio.")]
        public decimal? Amount { get; set; }

        [Required(ErrorMessage = "El campo justificación es obligatorio.")]
        [StringLength(25, ErrorMessage = "El campo justificación no puede tener más de 200 caracteres.")]
        public string Justification { get; set; } = null!;

        [Required(ErrorMessage = "El campo empleado es obligatorio.")]
        public int? UserId { get; set; }

        [Required(ErrorMessage = "El campo tipo de moneda es obligatorio.")]
        public byte? TypeCurrencyId { get; set; }

        [Required(ErrorMessage = "El campo cuenta es obligatorio.")]
        public string AccountId { get; set; } = null!;

        [Required(ErrorMessage = "El campo plazo es obligatorio.")]
        public byte? DeadlineId { get; set; }

        [Required(ErrorMessage = "El campo tipo de prestamo es obligatorio.")]
        public byte? TypeLoanId { get; set; }

        [Required(ErrorMessage = "El campo interes es obligatorio.")]
        public byte? InterestId { get; set; }
    }
}