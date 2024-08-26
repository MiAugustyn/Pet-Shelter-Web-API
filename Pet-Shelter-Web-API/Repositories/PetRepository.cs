using Pet_Shelter_Web_API.Interfaces;
using PetShelterWebAPI.Data;
using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly DataContext _context;

        public PetRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Pet> GetPets()
        {
            return _context.Pets.OrderBy(p => p.Id).ToList();
        }

        public ICollection<Pet> GetPets(string name)
        {
            return _context.Pets.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
        }

        public Pet GetPet(int id)
        {
            return _context.Pets.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Note> GetNotesByPet(int petId)
        {
            return _context.Pets.Where(p => p.Id == petId).SelectMany(p => p.Notes).ToList();
        }

        public Specie GetSpecieByPet(int petId)
        {
            return _context.Pets.Where(p => p.Id == petId).Select(p => p.Specie).FirstOrDefault();
        }

        public Breed GetBreedByPet(int petId)
        {
            return _context.Pets.Where(p => p.Id == petId).Select(p => p.Breed).FirstOrDefault();
        }

        public bool PetExists(int id)
        {
            return _context.Pets.Any(p => p.Id == id);
        }

        public bool PetExists(string name)
        {
            return _context.Pets.Any(p => p.Name.ToLower() == name.ToLower());
        }
    }
}