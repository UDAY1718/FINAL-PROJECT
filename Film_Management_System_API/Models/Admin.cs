namespace Film_Management_System_API.Models
{
    public class Admin
    {
        public int AdminId { get; set; }    
        public string AdminUsernameEmail { get; set; }
        public string AdminPassword { get; set; }   
        public byte[] AdminPasswordHash { get; set; }
        public byte[] AdminPasswordSalt {  get; set; }
    }
}
