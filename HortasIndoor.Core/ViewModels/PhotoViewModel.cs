using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HortasIndoor.Core.ViewModels
{
    public class PhotoViewModel
    {
        public string Description { get; set; }
        [JsonIgnore]
        [Display(Name = "Arquivo da foto")]
        public IFormFile? File { get; set; }
    }
}
