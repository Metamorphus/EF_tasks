using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Week7_1.Migrations;

namespace Week7_1
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./blog.db");
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                db.Database.Migrate();

                var blog1 = new Blog
                {
                    Url = "http://blogs.msdn.com/dotnet",
                    Name = ".NET tutorials",
                    Posts = new List<Post>
                    {
                        new Post { Title = "Intro to C#" },
                        new Post { Title = "Intro to VB.NET" },
                        new Post { Title = "Intro to F#" }
                    }
                };

                var blog2 = new Blog
                {
                    Url = "http://vk.com/morozov",
                    Name = "History of my life",
                    Posts = new List<Post>
                    {
                        new Post { Title = "My sad childhood" },
                        new Post { Title = "My sad youth" },
                        new Post { Title = "My happy acquaintance with C#" }
                    }
                };

                db.Blogs.Add(blog1);
                db.Blogs.Add(blog2);
                db.SaveChanges();
            }

            using (var db = new BloggingContext())
            {
                var allBlogs = db.Blogs.ToList();
                Console.WriteLine("Query 1:");
                foreach (var blog in allBlogs)
                {
                    Console.WriteLine(blog.Name);
                }
                Console.WriteLine();

                var dotnetBlog = db.Blogs
                    .Single(b => b.Url.Contains("dotnet"));
                Console.WriteLine("Query 2: {0}", dotnetBlog.Name);
                Console.WriteLine();

                var historyWithPosts = db.Blogs
                    .Where(b => b.Name.Contains("History"))
                    .Include(b => b.Posts)
                    .Single();
                Console.WriteLine("Query 3:");
                foreach (var post in historyWithPosts.Posts)
                {
                    Console.WriteLine(post.Title);
                }
                Console.WriteLine();

                var blogsWithCsharp = db.Posts
                    .Where(p => p.Title.Contains("C#"))
                    .Include(p => p.Blog)
                    .ToList();
                Console.WriteLine("Query 4:");
                foreach (var blog in blogsWithCsharp)
                {
                    Console.WriteLine(blog.Blog.Name);
                }
                Console.WriteLine();
            }
        }
    }
}
