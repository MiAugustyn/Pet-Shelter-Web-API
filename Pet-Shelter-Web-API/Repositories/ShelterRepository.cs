using AutoMapper;
using Pet_Shelter_Web_API.Interfaces;
using PetShelterWebAPI.Data;
using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Repositories
{
    public class ShelterRepository : IShelterRepository
    {
        private readonly DataContext _context;

        public ShelterRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Shelter> GetShelters()
        {
            return _context.Shelters.OrderBy(s => s.Id).ToList();
        }

        public Shelter GetShelter(int id)
        {
            return _context.Shelters.Where(s => s.Id == id).FirstOrDefault();
        }

        public ICollection<Worker> GetWorkersByShelter(int shelterId)
        {
            return _context.Shelters.Where(s => s.Id == shelterId).SelectMany(s => s.Workers).ToList();
        }

        public ICollection<Pet> GetPetsByShelter(int shelterId)
        {
            return _context.Shelters.Where(s => s.Id == shelterId).SelectMany(s => s.Pets).ToList();
        }

        public bool ShelterExists(int id)
        {
            return _context.Shelters.Any(s => s.Id == id);
        }

        public bool CreateShelter(Shelter shelter)
        {
            _context.Add(shelter);
            return Save();
        }

        public bool Save()
        {
            var Save = _context.SaveChanges();
            return Save > 0 ? true : false;
        }
    }
}
