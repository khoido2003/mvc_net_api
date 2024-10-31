using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
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
    public async Task<ActionResult<IEnumerable<Command>>> GetAllCommands()
    {
      var commandItems = await this._repository.GetAllCommands();

      Console.WriteLine(commandItems);
      return Ok(commandItems);
    }

    // GET api/commands/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CommandReadDto>> GetCommandById(int id)
    {
      var commandItem = await this._repository.GetCommandById(id);
      if (commandItem == null)
      {
        return NotFound();
      }
      return Ok(_mapper.Map<CommandReadDto>(commandItem));
    }
  }
}