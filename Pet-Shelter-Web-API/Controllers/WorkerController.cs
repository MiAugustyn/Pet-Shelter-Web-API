using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pet_Shelter_Web_API.DTO;
using Pet_Shelter_Web_API.Interfaces;
using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Controllers
{
    [Route("api / [controller]")]
    [ApiController]
    public class WorkerController : Controller
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IMapper _mapper;

        public WorkerController(IWorkerRepository workerRepository, IMapper mapper)
        {
            _workerRepository = workerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Worker>))]
        public IActionResult GetWorkers()
        {
            var Workers = _mapper.Map<List<WorkerDTO>>(_workerRepository.GetWorkers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Workers);
        }

        [HttpGet("ByName/{WorkerName}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Worker>))]
        [ProducesResponseType(400)]
        public IActionResult GetWorkers(string WorkerName)
        {
            if (!_workerRepository.WorkerExists(WorkerName))
            {
                return NotFound();
            }

            var Workers = _mapper.Map<List<WorkerDTO>>(_workerRepository.GetWorkers(WorkerName));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Workers);
        }

        [HttpGet("ById/{WorkerId}")]
        [ProducesResponseType(200, Type = typeof(Worker))]
        [ProducesResponseType(400)]
        public IActionResult GetWorker(int WorkerId)
        {
            if (!_workerRepository.WorkerExists(WorkerId))
            {
                return NotFound();
            }

            var Worker = _mapper.Map<WorkerDTO>(_workerRepository.GetWorker(WorkerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Worker);
        }


        [HttpGet("NotesByWorker/{WorkerId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Note>))]
        [ProducesResponseType(400)]
        public IActionResult GetNotesByWorker(int WorkerId)
        {
            if (!_workerRepository.WorkerExists(WorkerId))
            {
                return NotFound();
            }

            var Notes = _mapper.Map<List<NoteDTO>>(_workerRepository.GetNotesByWorker(WorkerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Notes);
        }
    }
}
