using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HortasIndoor.Core.ViewModels
{
    public class EditUserViewModel
    {
        public string Localizacao { get; set; }
        [Display(Name = "Foto de Avatar")]
        public IFormFile? File { get; set; }
    }
}
