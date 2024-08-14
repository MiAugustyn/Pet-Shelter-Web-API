namespace PetShelterWebAPI.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Shelter Shelter { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}
