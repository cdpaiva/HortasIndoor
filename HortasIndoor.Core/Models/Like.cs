using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HortasIndoor.Core.Models
{
    public class Like
    {
        public int Id { get; set; }
        [JsonIgnore]
        public Post? Post { get; set; }
        [JsonIgnore]
        public ApplicationUser? User { get; set; }
    }
}
