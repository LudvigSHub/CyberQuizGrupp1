using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CyberQuizGrupp1.SHARED.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Fyll i användarnamn")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Fyll i lösenord")]
        public string Password { get; set; }
    }
}
