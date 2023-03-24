using HortasIndoor.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortasIndoor.Core.Interfaces
{
    public interface IProfileRepository
    {
        public List<ApplicationUser> GetAll();
        public ApplicationUser GetById(string id);
        public Task<ApplicationUser> Update(ApplicationUser user);
    }
}
