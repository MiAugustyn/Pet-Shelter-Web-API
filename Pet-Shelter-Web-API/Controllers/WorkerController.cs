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
        private readonly IShelterRepository _shelterRepository;
        private readonly IMapper _mapper;

        public WorkerController(IWorkerRepository workerRepository, IShelterRepository shelterRepository, IMapper mapper)
        {
            _workerRepository = workerRepository;
            _shelterRepository = shelterRepository;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateWorker([FromQuery] int ShelterId, [FromBody] WorkerDTO NewWorker)
        {
            if (NewWorker == null || !_shelterRepository.ShelterExists(ShelterId))
            {
                return BadRequest(ModelState);
            }

            var WorkerCheck = _workerRepository.GetWorkers()
                .Where(w => w.Email.Trim().ToLower() == NewWorker.Email.ToLower())
                .FirstOrDefault();

            if (WorkerCheck != null)
            {
                ModelState.AddModelError("", "Worker already exists.");
                return StatusCode(422);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var NewWorkerMap = _mapper.Map<Worker>(NewWorker);

            NewWorkerMap.Shelter = _shelterRepository.GetShelter(ShelterId);

            if (!_workerRepository.CreateWorker(NewWorkerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving worker.");
                return StatusCode(500);
            }

            return Ok("Worker created successfully.");
        }

        [HttpPut("{WorkerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateWorker(int WorkerId, [FromQuery] int ShelterId, [FromBody] WorkerDTO UpdatedWorker)
        {
            if (UpdatedWorker == null)
            {
                return BadRequest(ModelState);
            }

            if (!_workerRepository.WorkerExists(WorkerId) || !_shelterRepository.ShelterExists(ShelterId))
            {
                return NotFound();
            }

            if (UpdatedWorker.Id != WorkerId)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var WorkerMap = _mapper.Map<Worker>(UpdatedWorker);

            WorkerMap.Shelter = _shelterRepository.GetShelter(ShelterId);

            if (!_workerRepository.UpdateWorker(WorkerMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating worker.");
                return StatusCode(500);
            }

            return NoContent();
        }
    }
}
