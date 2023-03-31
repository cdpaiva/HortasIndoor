using HortasIndoor.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortasIndoor.Core.Interfaces
{
    public interface IPostRepository
    {
        public Task<Post> Create(string Id, Post post);
        public Task<IEnumerable<Post>> GetAll();
        public Task<Post> AddLike(int postId, string userId);
    }
}
