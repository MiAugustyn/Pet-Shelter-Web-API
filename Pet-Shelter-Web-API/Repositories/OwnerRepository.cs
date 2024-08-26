using Pet_Shelter_Web_API.Interfaces;
using PetShelterWebAPI.Data;
using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.OrderBy(o => o.Id).ToList();
        }

        public ICollection<Owner> GetOwners(string name)
        {
            return _context.Owners.Where(o => o.Name.ToLower() == name.ToLower()).ToList();
        }

        public ICollection<Pet> GetPetsByOwner(int ownerId)
        {
            return _context.PetOwners.Where(po => po.Owner.Id == ownerId).Select(po => po.Pet).ToList();
        }

        public Owner GetOwner(int id)
        {
            return _context.Owners.Where(o => o.Id == id).FirstOrDefault();
        }

        public bool OwnerExists(int id)
        {
            return _context.Owners.Any(o => o.Id == id);
        }

        public bool OwnerExists(string name)
        {
            return _context.Owners.Any(o => o.Name == name);
        }
    }
}
