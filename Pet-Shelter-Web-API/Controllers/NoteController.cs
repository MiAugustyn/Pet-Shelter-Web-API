using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pet_Shelter_Web_API.DTO;
using Pet_Shelter_Web_API.Interfaces;
using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Controllers
{
    [Route("api / [controller]")]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;
        private readonly IWorkerRepository _workerRepository;
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;

        public NoteController(INoteRepository noteRepository,
            IWorkerRepository workerRepository,
            IPetRepository petRepository,
            IMapper mapper)
        {
            _noteRepository = noteRepository;
            _workerRepository = workerRepository;
            _petRepository = petRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Note>))]
        public IActionResult GetNotes()
        {
            var Notes = _mapper.Map<List<NoteDTO>>(_noteRepository.GetNotes());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Notes);
        }

        [HttpGet("ById/{NoteId}")]
        [ProducesResponseType(200, Type = typeof(Note))]
        [ProducesResponseType(400)]
        public IActionResult GetNote(int NoteId)
        {
            if (!_noteRepository.NoteExists(NoteId))
            {
                return NotFound();
            }

            var Note = _mapper.Map<NoteDTO>(_noteRepository.GetNote(NoteId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Note);
        }

        [HttpGet("WorkerByNote/{NoteId}")]
        [ProducesResponseType(200, Type = typeof(Worker))]
        [ProducesResponseType(400)]
        public IActionResult GetWorkerByNote(int NoteId)
        {
            if (!_noteRepository.NoteExists(NoteId))
            {
                return NotFound();
            }

            var Worker = _mapper.Map<WorkerDTO>(_noteRepository.GetWorkerByNote(NoteId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Worker);
        }

        [HttpGet("PetByNote/{NoteId}")]
        [ProducesResponseType(200, Type = typeof(Pet))]
        [ProducesResponseType(400)]
        public IActionResult GetPetByNote(int NoteId)
        {
            if (!_noteRepository.NoteExists(NoteId))
            {
                return NotFound();
            }

            var Pet = _mapper.Map<PetDTO>(_noteRepository.GetPetByNote(NoteId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Pet);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateNote([FromQuery] int WorkerId, [FromQuery] int PetId, [FromBody] NoteDTO NewNote)
        {
            if (NewNote == null || !_workerRepository.WorkerExists(WorkerId) || !_petRepository.PetExists(PetId))
            {
                return BadRequest(ModelState);
            }

            var NewNoteCheck = _noteRepository.GetNotes()
                .Where(n => n.Description.Trim().ToLower() == NewNote.Description.Trim().ToLower())
                .FirstOrDefault();

            if (NewNoteCheck != null)
            {
                ModelState.AddModelError("", "Note already exiss.");
                return StatusCode(422);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var NewNoteMap = _mapper.Map<Note>(NewNote);

            NewNoteMap.Worker = _workerRepository.GetWorker(WorkerId);
            NewNoteMap.Pet = _petRepository.GetPet(PetId);

            if (!_noteRepository.CreateNote(NewNoteMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving note.");
                return StatusCode(500);
            }

            return Ok("Note created successfully.");
        }

        [HttpPut("{NoteId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateNote(int NoteId, [FromQuery] int WorkerId, [FromQuery] int PetId, [FromBody] NoteDTO UpdatedNote)
        {
            if (UpdatedNote == null)
            {
                return BadRequest(ModelState);
            }

            if (!_noteRepository.NoteExists(NoteId) || !_workerRepository.WorkerExists(WorkerId) || _petRepository.PetExists(PetId))
            {
                return NotFound();
            }

            if (UpdatedNote.Id != NoteId)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var NoteMap = _mapper.Map<Note>(UpdatedNote);

            NoteMap.Worker = _workerRepository.GetWorker(WorkerId);
            NoteMap.Pet = _petRepository.GetPet(PetId);

            if (!_noteRepository.UpdateNote(NoteMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating note.");
                return StatusCode(500);
            }

            return NoContent();
        }
    }
}
