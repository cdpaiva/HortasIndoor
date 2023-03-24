using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortasIndoor.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Localizacao { get; set; }
        public List<Photo>? Photos { get; set; }
    }
}
