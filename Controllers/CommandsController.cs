using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
  // api/commands
  [Route("api/commands")]
  [ApiController]
  public class CommandsController : ControllerBase
  {
    private readonly ICommanderRepo _repository;
    private readonly IMapper _mapper;

    public CommandsController(ICommanderRepo repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    // GET api/commands
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetAllCommands()
    {
      var commandItems = await this._repository.GetAllCommands();

      Console.WriteLine(commandItems);
      return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
    }

    // GET api/commands/{id}
    [HttpGet("{id}", Name = "GetCommandById")]
    public async Task<ActionResult<CommandReadDto>> GetCommandById(int id)
    {
      var commandItem = await this._repository.GetCommandById(id);
      if (commandItem == null)
      {
        return NotFound();
      }
      return Ok(_mapper.Map<CommandReadDto>(commandItem));
    }


    // POST api/commands
    [HttpPost]
    public async Task<ActionResult<CommandReadDto>> CreateCommand(CommandCreateDto commandCreateDto)
    {
      var commandModel = _mapper.Map<Command>(commandCreateDto);
      await _repository.CreateCommand(commandModel);
      await _repository.SaveChanges();

      var commandReadDtoFormat = _mapper.Map<CommandReadDto>(commandModel);


      return CreatedAtRoute("GetCommandById", new { id = commandReadDtoFormat.Id }, commandReadDtoFormat);
      // return Ok(commandReadDtoFormat);
    }

    // PUT api/commands/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
    {

      var commandModelFromRepo = await _repository.GetCommandById(id);
      if (commandModelFromRepo == null)
      {
        return NotFound();
      }

      _mapper.Map(commandUpdateDto, commandModelFromRepo);
      _repository.UpdateCommand(commandModelFromRepo);

      await _repository.SaveChanges();
      return NoContent();
    }

    // PATCH api/commands/{id}
    [HttpPatch("{id}")]
    public async Task<ActionResult> PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
    {
      var commandModelFromRepo = await _repository.GetCommandById(id);
      if (commandModelFromRepo == null)
      {
        return NotFound();
      }

      var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
      patchDoc.ApplyTo(commandToPatch, ModelState);

      if (!TryValidateModel(commandToPatch))
      {
        return ValidationProblem(ModelState);
      }

      _mapper.Map(commandToPatch, commandModelFromRepo);

      _repository.UpdateCommand(commandModelFromRepo);

      await _repository.SaveChanges();

      return NoContent();
    }

    // [HttpDelete("{id}")]
    // public ActionResult DeleteCommand(int id)
    // {
    //   var commandModelFromRepo = _repository.GetCommandById(id);
    //   if (commandModelFromRepo == null)
    //   {
    //     return NotFound();
    //   }
    //   _repository.DeleteCommand(commandModelFromRepo);
    //   _repository.SaveChanges();

    //   return NoContent();
    // }
  }
}

