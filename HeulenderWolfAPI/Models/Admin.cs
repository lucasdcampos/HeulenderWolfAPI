using System.Text.Json.Serialization;

namespace HeulenderWolfAPI.Models
{
    public class Admin
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // EF Core requires a parameterless constructor
        public Admin() { }

        public Admin(string email, string passwordHash)
        {
            ID = Guid.NewGuid();
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
