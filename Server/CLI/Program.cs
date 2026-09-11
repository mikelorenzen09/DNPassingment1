using CLI.UI;
using FileRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI Application...");

IUserRepository userRepository = new UserFileRepository();
ICommentRepository commentRepository = new  CommentFileRepository();
IPostRepository postRepository = new PostFileRepository();

CliApp CliApp = new CliApp(userRepository, commentRepository, postRepository);
await CliApp.StartAsync();