using Pet_Shelter_Web_API.Interfaces;
using PetShelterWebAPI.Data;
using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Repositories
{
    public class SpecieRepository : ISpecieRepository
    {
        private readonly DataContext _context;

        public SpecieRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Specie> GetSpecies()
        {
            return _context.Species.OrderBy(s => s.Id).ToList();
        }

        public Specie GetSpecie(int id)
        {
            return _context.Species.Where(s => s.Id == id).FirstOrDefault();
        }

        public ICollection<Pet> GetPetsBySpecie(int specieId)
        {
            return _context.Species.Where(s => s.Id == specieId).SelectMany(s => s.Pets).ToList();
        }

        public bool SpecieExists(int id)
        {
            return _context.Species.Any(s => s.Id == id);
        }
    }
}
