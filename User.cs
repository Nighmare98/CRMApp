namespace CRMApp
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }

    public enum Role
    {
        Administrator,
        SalesManager,
        Support
    }
}
