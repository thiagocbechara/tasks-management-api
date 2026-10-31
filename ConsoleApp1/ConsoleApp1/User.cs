using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public Post[] Posts { get; set; }
    }

    class Post
    {
        public int UserId { get; set; }
        public string Title { get; set; }
    }
}
