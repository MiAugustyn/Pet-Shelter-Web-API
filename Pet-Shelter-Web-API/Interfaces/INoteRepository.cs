using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Interfaces
{
    public interface INoteRepository
    {
        ICollection<Note> GetNotes();
        Note GetNote(int id);
        Worker GetWorkerByNote(int noteId);
        Pet GetPetByNote(int noteId);
        bool NoteExists(int id);
        bool CreateNote(Note note);
        bool Save();
    }
}
