namespace Chirper.Server.Infrastructure
{
    public class BCryptPasswordHasher: IPasswordHasher
    {
        public string HashPassword(string clearTextPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(clearTextPassword);
        }

        public bool VerifyPassword(string clearTextPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(clearTextPassword, hashedPassword);
        }
    }
}
