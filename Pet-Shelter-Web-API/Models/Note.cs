namespace PetShelterWebAPI.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Pet Pet { get; set; }
        public Worker Worker { get; set; }
    }
}
