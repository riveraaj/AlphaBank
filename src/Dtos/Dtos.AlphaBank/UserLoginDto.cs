﻿using System.ComponentModel.DataAnnotations;

namespace Dtos.AlphaBank {
    public class UserLoginDto {

        [Required]
        [Display(Name = "Usuario")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = null!;
    }
}