using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.DTO
{
    public class WorkerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Shelter Shelter { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}
