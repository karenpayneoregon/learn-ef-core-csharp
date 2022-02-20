using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Saving.Data;
using Saving.Models;
using Saving.Utilities;

namespace Saving.Classes
{
    public class BlogPostSample
    {
        public static async Task FreshStart()
        {
            await using var context = new BloggingContext();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }

        public static async Task CreateNewPopulateRead()
        {
            static void ShowBlogs(BloggingContext context)
            {

                foreach (var blog in context.Blogs)
                {
                    Console.WriteLine($"{blog.BlogId,-3}{blog.Description,-30}{blog.Url}");

                    // never assume there are children
                    if (blog.Posts is not null)
                    {
                        foreach (var post in blog.Posts)
                        {
                            Console.WriteLine($"\t{post.PostId,-3}{post.Title}");
                            Console.WriteLine($"\t\t{post.Content}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No post");
                    }


                    Console.WriteLine();

                }
            }
            ConsoleColors.WriteHeader($"Running {nameof(CreateNewPopulateRead)}");
            await FreshStart();

            // create blogs and post
            await using var context = new BloggingContext();

            var blog1 = new Blog
            {
                Url = "http://blogs.msdn.com/dotnet/csharp",
                Description = "Developer blog",
                Posts = new List<Post>
                {
                    new() { Title = "Intro to C#", Content = "Basic C#"},
                    new() { Title = "Working with classes", Content = "Understanding classes"}
                }
            };

            var blog2 = new Blog
            {
                Url = "http://blogs.msdn.com/dotnet/vbnet",
                Description = "Developer blog",
                Posts = new List<Post> { new() { Title = "Intro to VB", Content = "Basic VB.NET" } }
            };

            var blog3 = new Blog
            {
                Url = "http://blogs.msdn.com/dotnet/fsharp",
                Description = "Developer blog",
                Posts = new List<Post> { new() { Title = "Intro to F#", Content = "Learn F#" } }
            };

            context.AddRange(blog1, blog2, blog3);

            await context.SaveChangesAsync();

            var blogListings = await context.Blogs.ToListAsync();

            //foreach (var blog in blogListings)
            //{
            //    Debug.WriteLine($"{blog.BlogId, -5}{blog.Description}");
            //    foreach (var blogPost in blog.Posts)
            //    {
            //        Debug.WriteLine($"\t{blogPost.PostId, -5}{blogPost.Title}");
            //    }
            //}
            ShowBlogs(context);
        }
        /// <summary>
        /// Important to keep the DbContext scoped properly for each operation
        /// and to understand how caching and change tracking works.
        /// 
        /// - Create three blogs with posts
        /// - Delete one blog
        /// - Edit one blog
        ///     - inspect
        ///     - force reload
        /// </summary>
        public static async Task DeleteAndModifyRecordIndividualContexts()
        {

            ConsoleColors.WriteHeader($"Running {nameof(DeleteAndModifyRecordIndividualContexts)}");

            /* - local method
             * Iterate all blogs/post
             */
            static void ShowBlogs(BloggingContext context)
            {

                foreach (var blog in context.Blogs)
                {
                    Console.WriteLine($"{blog.BlogId, -3}{blog.Description,-30}{blog.Url}");

                    // never assume there are children
                    if (blog.Posts is not null)
                    {
                        foreach (var post in blog.Posts)
                        {
                            Console.WriteLine($"\t{post.PostId,-3}{post.Title}");
                            Console.WriteLine($"\t\t{post.Content}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No post");
                    }


                    Console.WriteLine();

                }
            }

            /* - local method
             * Update a single blog url which the caller will
             * not know about see in this case the caller reloads
             * said blog entry.
             */
            static void UpdateBlogTitle(int blogIdentifier)
            {
                using var context = new BloggingContext();
                var blog = context.Blogs.FirstOrDefault(b => b.BlogId == blogIdentifier);
                blog.Url = "https://csharpforums.net/";
                context.SaveChanges();
            }

            // start fresh
            await using (var context = new BloggingContext())
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
            }

            // create blogs and post
            await using (var context = new BloggingContext())
            {
                var blog1 = new Blog
                {
                    Url = "http://blogs.msdn.com/dotnet/csharp", 
                    Description = "Developer blog",
                    Posts = new List<Post>
                    {
                        new() { Title = "Intro to C#", Content = "Basic C#"},
                        new() { Title = "Working with classes", Content = "Understanding classes"}
                    }
                };

                var blog2 = new Blog
                {
                    Url = "http://blogs.msdn.com/dotnet/vbnet",
                    Description = "Developer blog",
                    Posts = new List<Post> { new() { Title = "Intro to VB", Content = "Basic VB.NET"} }
                };

                var blog3 = new Blog
                {
                    Url = "http://blogs.msdn.com/dotnet/fsharp",
                    Description = "Developer blog",
                    Posts = new List<Post> { new() { Title = "Intro to F#", Content = "Learn F#"} }
                };

                context.AddRange(blog1, blog2, blog3);

                await context.SaveChangesAsync();
                ShowBlogs(context);

                Console.WriteLine($"Blog count after add {context.Blogs.Count()}");

                context.Blogs.Remove(blog2);
                await context.SaveChangesAsync();
                Console.WriteLine($"Blog count after remove one blog {context.Blogs.Count()}");

                int blogIdentifier = 1;

                // this update will not be seen for the current DbContext being tracked
                UpdateBlogTitle(blogIdentifier);

                var changedBlog = context.Blogs.FirstOrDefault(b => b.BlogId == blogIdentifier);
                Console.WriteLine($"After change: '{changedBlog.Url}'");

                /*
                 * to see the change
                 * Reloads the entity from the database overwriting any property values
                 * with values from the database.
                 *
                 * https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.changetracking.entityentry.reloadasync?view=efcore-6.0
                 *
                 */
                await context.Entry(changedBlog).ReloadAsync();

                Console.WriteLine($"After change reloaded: '{changedBlog.Url}' State: {context.Entry(changedBlog).State}");
                /*
                 * this is debatable dependent on business logic
                 */
                context.Entry(changedBlog).State = EntityState.Modified;
                Console.WriteLine($"After change reloaded: '{changedBlog.Url}' State: {context.Entry(changedBlog).State}");

            }

            // simple inspection
            await using (var context = new BloggingContext())
            {
                Console.WriteLine($"Blog identifiers {string.Join(",", context.Blogs.Select(blog => blog.BlogId))}");
                Console.WriteLine($"Blog count {context.Blogs.Count()}");
            }

            Console.WriteLine();

        }

    }
}