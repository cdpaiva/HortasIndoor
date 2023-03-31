using HortasIndoor.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortasIndoor.Core.ViewModels
{
    public class InitialPageViewModel
    {
        public InitialPageViewModel(ApplicationUser user, IEnumerable<Post> posts) 
        { 
            this.Posts = posts;
            this.User = user;
        }

        public IEnumerable<Post> Posts { get; set; }
        public ApplicationUser User { get; set; }
    }
}
