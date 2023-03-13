using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortasIndoor.Core.ViewModels
{
    public class LoginCredentialsViewModel
    {
        [Required(ErrorMessage = "O campo Username deve ser preenchido")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Por favor, insira seu Password")]
        public string? Password { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
