using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Interfaces
{
    public interface IWorkerRepository
    {
        ICollection<Worker> GetWorkers();
        ICollection<Worker> GetWorkers(string name);
        Worker GetWorker(int id);
        ICollection<Note> GetNotesByWorker(int workerId);
        bool WorkerExists(int id);
        bool WorkerExists(string name);
        bool CreateWorker(Worker worker);
        bool UpdateWorker(Worker worker);
        bool Save();
    }
}
