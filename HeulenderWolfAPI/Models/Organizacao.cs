using System.Text.Json.Serialization;

namespace HeulenderWolfAPI.Models
{
    public class Organizacao
    {
        public Guid ID { get; set; }
        public string CNPJ { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property: one organization -> many pets
        public ICollection<Pet> Pets { get; set; }

        public Organizacao() { }

        public Organizacao(string cnpj, string passwordHash)
        {
            ID = Guid.NewGuid();
            CNPJ = cnpj;
            PasswordHash = passwordHash;
        }
    }
}
