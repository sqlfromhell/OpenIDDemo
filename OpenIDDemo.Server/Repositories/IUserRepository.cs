namespace OpenIDDemo.Server.Repositories;

public interface IUserRepository
{
    User Get(string userName, string password);
}