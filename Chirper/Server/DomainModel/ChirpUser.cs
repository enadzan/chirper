namespace Chirper.Server.DomainModel
{
    public class ChirpUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string UsernameNormalized { get; set; }
        public string Password { get; set; }
        public string SecurityStamp { get; set; }
    }
}
