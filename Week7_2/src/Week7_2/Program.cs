using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Week7_2
{
    public class CmsContext : DbContext
    {
        public DbSet<Page> Pages { get; set; }
        public DbSet<NavLink> Links { get; set; }
        public DbSet<RelatedPage> RelatedPages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./cms.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Page>()
                .Property(p => p.AddedDate)
                .HasDefaultValueSql("DATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<NavLink>()
                .HasOne(link => link.ParentPage)
                .WithMany(page => page.OutcomingLinks);

            modelBuilder.Entity<NavLink>()
                .HasOne(link => link.Page)
                .WithMany(page => page.IncomingLinks);

            modelBuilder.Entity<RelatedPage>()
                .HasOne(rel => rel.FirstPage)
                .WithMany(page => page.OutcomingRelations);

            modelBuilder.Entity<RelatedPage>()
                .HasOne(rel => rel.SecondPage)
                .WithMany(page => page.IncomingRelations);
        }
    }

    public class Page
    { 
        public int PageId { get; set; }
        [Required]
        public string UrlName { get; set; }
        [Required]
        public string Content { get; set; }

        public string Description { get; set; }
        public string Title { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime AddedDate { get; set; }

        public List<NavLink> OutcomingLinks { get; set; } = new List<NavLink>();
        public List<NavLink> IncomingLinks { get; set; } = new List<NavLink>();

        public List<RelatedPage> OutcomingRelations { get; set; } = new List<RelatedPage>();
        public List<RelatedPage> IncomingRelations { get; set; } = new List<RelatedPage>();

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("PageId: ");
            builder.Append(PageId.ToString());
            builder.Append(", UrlName: ");
            builder.Append(UrlName);
            builder.Append(", Content: ");
            builder.Append(Content + "\n");

            builder.Append("Has outcoming links for: ");
            foreach (var link in OutcomingLinks)
            {
                builder.Append(link.PageId + " ");
            }

            builder.Append("\nHas incoming links for: ");
            foreach (var link in IncomingLinks)
            {
                builder.Append(link.ParentPageId + " ");
            }

            builder.Append("\nHas outcoming relations for: ");
            foreach (var rel in OutcomingRelations)
            {
                builder.Append(rel.FirstPageId + " ");
            }

            builder.Append("\nHas incoming relations for: ");
            foreach (var rel in IncomingRelations)
            {
                builder.Append(rel.SecondPageId + " ");
            }
            builder.Append("\n");

            return builder.ToString();
        }
    }

    public class NavLink
    {
        public int NavLinkId { get; set; }

        [Required]
        public int ParentPageId { get; set; }
        [ForeignKey("ParentPageId")]
        public Page ParentPage { get; set; }

        [Required]
        public int PageId { get; set; }
        [ForeignKey("PageId")]
        public Page Page { get; set; } 

        public string Title { get; set; }
        public int Position { get; set; }
    }

    public class RelatedPage
    {
        [Key]
        public int RelationId { get; set; }

        [Required]
        public int FirstPageId { get; set; }
        [ForeignKey("FirstPageId")]
        public Page FirstPage { get; set; }

        [Required]
        public int SecondPageId { get; set; }
        [ForeignKey("SecondPageId")]
        public Page SecondPage { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            if (File.Exists("cms.db"))
            {
                File.Delete("cms.db");
            }
            File.Create("cms.db");

            Console.WriteLine("WELCOME TO CMS DATA DOMAIN");
            Console.WriteLine("Usage examples:");
            Console.WriteLine("add Pages {UrlName: <url>, Content: <content>}");
            Console.WriteLine("add NavLinks {ParentPageId: <id1>, PageId: <id2>}");
            Console.WriteLine("add RelatedPages {FirstPageId: <id1>, SecondPageId: <id2>}");
            Console.WriteLine("update Pages <id> {UrlName: <url>, Content: <content>}");
            Console.WriteLine("update NavLinks <id> {ParentPageId: <id1>, PageId: <id2>}");
            Console.WriteLine("update RelatedPages <id> {FirstPageId: <id1>, SecondPageId: <id2>}");
            Console.WriteLine("delete Pages <id>");
            Console.WriteLine("delete NavLinks <id>");
            Console.WriteLine("delete RelatedPages <id>");
            Console.WriteLine("list all");
            Console.WriteLine("quit");
            Console.WriteLine();

            using (var db = new CmsContext())
            {
                db.Database.Migrate();

                try
                {
                    while (true)
                    {
                        Console.Write("> ");
                        string command = Console.ReadLine();
                        if (command.Equals("quit"))
                        {
                            break;
                        }
                        else
                        {
                            var firstWord = command.Substring(0, command.IndexOf(" "));
                            if (firstWord.Equals("list"))
                            {
                                listAll(db);
                            }
                            else if (firstWord.Equals("add"))
                            {
                                Match m = Regex.Match(command, @"add (\w+) (.*)");
                                if (m.Groups[1].Value.Equals("Pages"))
                                {
                                    Page page = JsonConvert.DeserializeObject<Page>(m.Groups[2].Value);
                                    AddPage(db, page);
                                }
                                else if (m.Groups[1].Value.Equals("NavLinks"))
                                {
                                    NavLink link = JsonConvert.DeserializeObject<NavLink>(m.Groups[2].Value);
                                    AddLink(db, link);
                                }
                                else if (m.Groups[1].Value.Equals("RelatedPages"))
                                {
                                    RelatedPage relation = JsonConvert.DeserializeObject<RelatedPage>(m.Groups[2].Value);
                                    AddRelation(db, relation);
                                }
                                else throw new Exception();
                                Console.WriteLine("Add operation succeeded");
                            }
                            else if (firstWord.Equals("update"))
                            {
                                Match m = Regex.Match(command, @"update (\w+) (\d+) (.*)");
                                if (m.Groups[1].Value.Equals("Pages"))
                                {
                                    Page page = JsonConvert.DeserializeObject<Page>(m.Groups[3].Value);
                                    UpdatePage(db, Int32.Parse(m.Groups[2].Value), page);
                                }
                                else if (m.Groups[1].Value.Equals("NavLinks"))
                                {
                                    NavLink link = JsonConvert.DeserializeObject<NavLink>(m.Groups[3].Value);
                                    UpdateLink(db, Int32.Parse(m.Groups[2].Value), link);
                                }
                                else if (m.Groups[1].Value.Equals("RelatedPages"))
                                {
                                    RelatedPage relation = JsonConvert.DeserializeObject<RelatedPage>(m.Groups[3].Value);
                                    UpdateRelation(db, Int32.Parse(m.Groups[2].Value), relation);
                                }
                                else throw new Exception();
                                Console.WriteLine("Update operation succeeded");
                            }
                            else if (firstWord.Equals("delete"))
                            {
                                Match m = Regex.Match(command, @"delete (\w+) (\d+)");
                                if (m.Groups[1].Value.Equals("Pages"))
                                {
                                    DeletePage(db, Int32.Parse(m.Groups[2].Value));
                                }
                                else if (m.Groups[1].Value.Equals("NavLinks"))
                                {
                                    DeleteLink(db, Int32.Parse(m.Groups[2].Value));
                                }
                                else if (m.Groups[1].Value.Equals("RelatedPages"))
                                {
                                    DeleteRelation(db, Int32.Parse(m.Groups[2].Value));
                                }
                                else throw new Exception();
                                Console.WriteLine("Delete operation succeeded");
                            }
                            else throw new Exception();
                        }
                    }
                }
                catch (JsonReaderException)
                {
                    Console.WriteLine("Wrong JSON format");
                }
                /*catch (Exception)
                {
                    Console.WriteLine("Something went wrong. Terminating the program");
                }*/
            }
        }

        public static void listAll(CmsContext db)
        {
            Console.WriteLine("PAGES: ");
            var pages = db.Pages.Include(p => p.IncomingLinks).Include(p => p.OutcomingLinks).ToList();
            foreach (var page in pages)
            {
                Console.WriteLine(page.ToString());
            }

            Console.WriteLine("LINKS: ");
            var links = db.Links.ToList();
            foreach (var link in links)
            {
                Console.WriteLine("Link " + link.NavLinkId + " from " + link.ParentPageId + 
                    " to " + link.PageId);
            }

            Console.WriteLine("PAGE RELATIONS: ");
            var rels = db.RelatedPages.ToList();
            foreach (var rel in rels)
            {
                Console.WriteLine("Relation "+ rel.RelationId + " between pages " + rel.FirstPageId + 
                    " and " + rel.SecondPageId);
            }
        }

        public static void AddPage(CmsContext db, Page page)
        {
            db.Pages.Add(page);
            db.SaveChanges();
        }

        public static void AddLink(CmsContext db, NavLink link)
        {
            db.Links.Add(link);
            db.SaveChanges();
        }

        public static void AddRelation(CmsContext db, RelatedPage relation)
        {
            db.RelatedPages.Add(relation);
            db.SaveChanges();
        }

        public static void UpdatePage(CmsContext db, int id, Page newPage)
        {
            var oldPage = db.Pages.Where(p => p.PageId == id).Single();
            oldPage.UrlName = newPage.UrlName;
            if (newPage.Content != null) oldPage.Content = newPage.Content;
            if (newPage.Description != null) oldPage.Description = newPage.Description;
            if (newPage.Title != null) oldPage.Title = newPage.Title;
            db.SaveChanges();
        }

        public static void UpdateLink(CmsContext db, int id, NavLink newLink)
        {
            var oldLink = db.Links.Where(p => p.NavLinkId == id).Single();
            oldLink.PageId = newLink.PageId;
            oldLink.ParentPageId = newLink.ParentPageId;
            oldLink.Position = newLink.Position;
            if (newLink.Title != null) oldLink.Title = newLink.Title;
            db.SaveChanges();
        }

        public static void UpdateRelation(CmsContext db, int id, RelatedPage newRelation)
        {
            var oldRelation = db.RelatedPages.Where(p => p.RelationId == id).Single();
            oldRelation.FirstPageId = newRelation.FirstPageId;
            oldRelation.SecondPageId = newRelation.SecondPageId;
            db.SaveChanges();
        }

        public static void DeletePage(CmsContext db, int id)
        {
            var upage = db.Pages.Where(p => p.PageId == id)
                    .Include(p => p.IncomingLinks)
                    .Include(p => p.OutcomingLinks).Single();
            db.Pages.Remove(upage);
            db.SaveChanges();
        }

        public static void DeleteLink(CmsContext db, int id)
        {
            var li = db.Links.Where(l => l.NavLinkId == id).Single();
            db.Links.Remove(li);
            db.SaveChanges();
        }

        public static void DeleteRelation(CmsContext db, int id)
        {
            var rel = db.RelatedPages.Where(l => l.RelationId == id).Single();
            db.RelatedPages.Remove(rel);
            db.SaveChanges();
        }
    }
}
