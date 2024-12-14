using Pet_Shelter_Web_API.Interfaces;
using PetShelterWebAPI.Data;
using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly DataContext _context;

        public WorkerRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Worker> GetWorkers()
        {
            return _context.Workers.OrderBy(w => w.Id).ToList();
        }

        public ICollection<Worker> GetWorkers(string name)
        {
            return _context.Workers.Where(w => w.Name.ToLower() == name.ToLower()).ToList();
        }

        public Worker GetWorker(int id)
        {
            return _context.Workers.Where(w => w.Id == id).FirstOrDefault();
        }

        public ICollection<Note> GetNotesByWorker(int workerId)
        {
            return _context.Workers.Where(w => w.Id == workerId).SelectMany(w => w.Notes).ToList();
        }

        public bool WorkerExists(int id)
        {
            return _context.Workers.Any(w => w.Id == id);
        }

        public bool WorkerExists(string name)
        {
            return _context.Workers.Any(w => w.Name == name);
        }

        public bool CreateWorker(Worker worker)
        {
            _context.Add(worker);
            return Save();
        }

        public bool UpdateWorker(Worker worker)
        {
            _context.Update(worker);
            return Save();
        }

        public bool DeleteWorker(Worker worker)
        {
            _context.Remove(worker);
            return Save();
        }

        public bool Save()
        {
            var Save = _context.SaveChanges();
            return Save > 0 ? true : false;
        }
    }
}
