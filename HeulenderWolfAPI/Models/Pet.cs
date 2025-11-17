namespace HeulenderWolfAPI.Models
{
    public enum Species
    {
        Dog,
        Cat,
        Bird,
        Rabbit,
        Dinosaur,
        Other
    }

    public class Pet
    {
        public Guid ID { get; set; }
        public Guid OrganizacaoID { get; set; }

        // Navigation property for EF Core
        public Organizacao Organizacao { get; set; }

        public string Name { get; set; }
        public Species Species { get; set; }
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Pet() { }

        public Pet(Guid organizacaoID, string name, Species species, int age)
        {
            ID = Guid.NewGuid();
            OrganizacaoID = organizacaoID;
            Name = name;
            Species = species;
            Age = age;
        }
    }
}
