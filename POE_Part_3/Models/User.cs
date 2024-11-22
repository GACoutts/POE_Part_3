namespace POE_Part_3.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ContactInfo { get; set; }
        public string Role { get; set; } // Admin, User, HR
    }
}
