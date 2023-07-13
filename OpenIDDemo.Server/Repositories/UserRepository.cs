namespace OpenIDDemo.Server.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users;

    public UserRepository()
    {
        _users = new List<User>
        {
            new User { Id = 1, UserName = "john", Password = "password" },
            new User { Id = 2, UserName = "jane", Password = "123456" }
        };
    }

    public User Get(string userName, string password)
        => _users
            .Where(e => e.UserName == userName)
            .Where(e => e.Password == password)
            .FirstOrDefault();
}