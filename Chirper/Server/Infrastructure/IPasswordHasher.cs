namespace Chirper.Server.Infrastructure
{
    public interface IPasswordHasher
    {
        string HashPassword(string clearTextPassword);
    }
}
