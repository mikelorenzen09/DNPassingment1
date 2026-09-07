

namespace CLI.UI.ManagePosts;
using Entities;
using System;
using RepositoryContracts;
public class CreatePostView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;
    public CreatePostView(IPostRepository postRepository,  IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Enter Title");
        string? title = Console.ReadLine();
        
        Console.WriteLine("Enter PostDescription");
        string? body = Console.ReadLine();
        
        Console.WriteLine("Enter userID");
        int userID = int.Parse(Console.ReadLine());
        var user = userRepository.GetMany().SingleOrDefault(u => u.UserID == userID);
        if (user is null)
        {
            Console.WriteLine("No user with that ID exists.");
            return;
        }

        Post newPost = new Post
        {
            Title = title, Body = body, UserID =  userID
        };
        Post createdPost = await postRepository.AddAsync(newPost);
        Console.WriteLine($"Post '{createdPost.Title}' created with ID {createdPost.PostID}.");
    }
}