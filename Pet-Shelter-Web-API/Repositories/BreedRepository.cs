using Pet_Shelter_Web_API.Interfaces;
using PetShelterWebAPI.Data;
using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Repositories
{
    public class BreedRepository : IBreedRepository
    {
        private readonly DataContext _context;

        public BreedRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Breed> GetBreeds()
        {
            return _context.Breeds.OrderBy(b => b.Id).ToList();
        }

        public Breed GetBreed(int id)
        {
            return _context.Breeds.Where(b => b.Id == id).FirstOrDefault();
        }

        public ICollection<Pet> GetPetsByBreed(int breedId)
        {
            return _context.Breeds.Where(b => b.Id == breedId).SelectMany(b => b.Pets).ToList();
        }

        public bool BreedExists(int id)
        {
            return _context.Breeds.Any(b => b.Id == id);
        }

        public bool CreateBreed(Breed breed)
        {
            _context.Add(breed);
            return Save();
        }

        public bool UpdateBreed(Breed breed)
        {
            _context.Update(breed);
            return Save();
        }

        public bool Save()
        {
            var Save = _context.SaveChanges();
            return Save > 0 ? true : false;
        }
    }
}
