﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortasIndoor.Core.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Created_at { get; set; }
        public ApplicationUser? User { get; set; }
        public List<Like>? Likes { get; set; }
    }
}
