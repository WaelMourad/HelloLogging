using System;

namespace HelloLogging.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
    }
}