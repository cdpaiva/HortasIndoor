using HortasIndoor.Core.Interfaces;
using HortasIndoor.Core.Models;
using HortasIndoor.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortasIndoor.DataAccess.Service
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly HIContext _context;

        public ProfileRepository(HIContext context)
        {
            this._context = context;
        }

        public List<ApplicationUser> GetAll()
        {
            return _context.ApplicationUsers.ToList();
        }

        public ApplicationUser Get(string id)
        {
            return _context.ApplicationUsers.Where(u => u.Id == id).First();
        }
    }
}
