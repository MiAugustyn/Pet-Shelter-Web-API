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
        private readonly IMapper _mapper;

        public NoteController(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
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
    }
}
