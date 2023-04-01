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

        public ApplicationUser GetById(string id)
        {
            return _context.ApplicationUsers.Where(u => u.Id == id).First();
        }

        public async Task<ApplicationUser> Update(ApplicationUser user)
        {
            var currentUser = _context.ApplicationUsers.Where(u => u.Id == user.Id).First();
            if(user.Localizacao!=null)
            {
                currentUser.Localizacao = user.Localizacao;
            }
            if(user.Avatar!=null)
            {
                currentUser.Avatar = user.Avatar;
            }
            _context.Update(currentUser);
            await _context.SaveChangesAsync();

            return currentUser;
        }

        public async Task<ApplicationUser> AddPhoto(string id, Photo photo)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                if (user.Photos == null)
                {
                    user.Photos = new List<Photo>();
                }
                user.Photos.Add(photo);
                await _context.SaveChangesAsync();
            }
            return user;
        }

        public async Task<ApplicationUser> GetWithGallery(string id)
        {
            return await _context.Users.Include(u=>u.Photos).FirstOrDefaultAsync(u => u.Id==id);
        }
    }
}
