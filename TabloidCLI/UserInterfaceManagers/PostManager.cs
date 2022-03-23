using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private string _connectionString;

      
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private NoteRepository _noteRepository;


        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _noteRepository = new NoteRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Posts");
            Console.WriteLine(" 3) Edit Posts");
            Console.WriteLine(" 4) Remove Posts");
            Console.WriteLine(" 5) Note Management");
            Console.WriteLine(" 6) Post Details");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                //these cases don't exist yet
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "5":
                    Console.WriteLine("Here is a list of your Notes");
                    List<Note> notes = _noteRepository.GetAll();
                    foreach (Note n in notes)
                    {
                        Console.WriteLine($"{n.Id} - Note: {n.Content}");
                    }
                    Console.Write("Press any key to continue");
                    Console.ReadKey();
                    return this;
                case "6":
                   Post post = ChoosePost();
                   if (post == null)
                   {
                       return this;
                   }
                   else
                   {
                       return new PostDetailManager(this, _connectionString, post.Id);
                   }
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine(post);
            }
        }

        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("A URL: ");
            post.Url = Console.ReadLine();

            Console.Write("Publication Date: ");
            post.PublishDateTime = DateTime.Parse(Console.ReadLine());

            Console.Write("Author: ");
            Author authorToAdd = Choose("Which author would you like to add?");
            post.Author = authorToAdd;

            Console.Write("Blog Title: ");
            Blog blogToAdd = ChooseBlog("Which blog would you like to add? ");
            post.Blog = blogToAdd;

            _postRepository.Insert(post);
        }

        private Author Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Author:";
            }

            Console.WriteLine(prompt);

            List<Author> authors = _authorRepository.GetAll();

            for (int i = 0; i < authors.Count; i++)
            {
                Author author = authors[i];
                Console.WriteLine($" {i + 1}) {author.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return authors[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
        private Blog ChooseBlog(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Blog:";
            }

            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1}) {blog.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private Author ChooseToEdit(string prompt = null)
        {
            Console.WriteLine(prompt);

            List<Author> authors = _authorRepository.GetAll();

            for (int i = 0; i < authors.Count; i++)
            {
                Author author = authors[i];
                Console.WriteLine($" {i + 1}) {author.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                int choice = int.Parse(input);
                return authors[choice - 1];
            }
            else
            {
                return null;
            }

        }
        private Blog ChooseBlogToEdit(string prompt = null)
        {
            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1}) {blog.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            else
            {
                return null;
            }

        }

        private Post ChoosePost(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Edit()
        {
            Post postToEdit = ChoosePost("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Post Title (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.Write("New url (blank to leave unchanged):  ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }
            Console.Write("New publication date (blank to leave unchanged:) ");
            string publicationDate = (Console.ReadLine());
            if (!string.IsNullOrWhiteSpace(publicationDate))
            {
                postToEdit.PublishDateTime = DateTime.Parse(publicationDate);
            }



            Author authorToEdit = ChooseToEdit("Please select the author of this post? (blank to leave unchanged):");
            if (authorToEdit != null)
            {
                postToEdit.Author = authorToEdit;
            }


            Blog blogToEdit = ChooseBlogToEdit("Please Select the blog for this post? (blank to leave unchanged): ");
            if (blogToEdit != null)
            {
                postToEdit.Blog = blogToEdit;
            }



            _postRepository.Update(postToEdit);
        }
        private void Remove()
        {
            Post postToDelete = ChoosePost("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }



    }
}
