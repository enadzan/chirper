namespace Chirper.Server.Infrastructure
{
    public interface IPasswordHasher
    {
        string HashPassword(string clearTextPassword);
        bool VerifyPassword(string clearTextPassword, string hashedPassword);
    }
}
