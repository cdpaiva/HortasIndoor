using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HortasIndoor.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Localizacao { get; set; }
        public List<Photo>? Photos { get; set; }
        public List<Like>? Likes { get; set; }
        [JsonIgnore]
        public List<Post>? Posts { get; set; }

    }
}
