using HortasIndoor.Core.Interfaces;
using HortasIndoor.Core.Models;
using HortasIndoor.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortasIndoor.DataAccess.Service
{
    public class PostRepository : IPostRepository
    {
        private readonly HIContext _context;

        public PostRepository(HIContext context)
        {
            this._context = context;
        }

        public async Task<Post> Create(string Id, Post post)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == Id);
            if (user != null)
            {
                post.User = user;
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
            }
            return post;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Posts.Include(p=>p.User).ToListAsync();
        }
    }
}
