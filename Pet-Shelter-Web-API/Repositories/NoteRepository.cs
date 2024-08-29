using Pet_Shelter_Web_API.Interfaces;
using PetShelterWebAPI.Data;
using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly DataContext _context;

        public NoteRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Note> GetNotes()
        {
            return _context.Notes.OrderBy(n => n.Id).ToList();
        }

        public Note GetNote(int id)
        {
            return _context.Notes.Where(n => n.Id == id).FirstOrDefault();
        }

        public Pet GetPetByNote(int noteId)
        {
            return _context.Notes.Where(n => n.Id == noteId).Select(n => n.Pet).FirstOrDefault();
        }

        public Worker GetWorkerByNote(int noteId)
        {
            return _context.Notes.Where(n => n.Id == noteId).Select(n => n.Worker).FirstOrDefault();
        }

        public bool NoteExists(int id)
        {
            return _context.Notes.Any(n => n.Id == id);
        }

        public bool CreateNote(Note note)
        {
            _context.Add(note);
            return Save();
        }

        public bool Save()
        {
            var Save = _context.SaveChanges();
            return Save > 0 ? true : false;
        }
    }
}
