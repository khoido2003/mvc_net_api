using Commander.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{
  public class CommanderRepo : ICommanderRepo
  {
    private readonly CommanderContext _context;

    public CommanderRepo(CommanderContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Command>> GetAllCommands()
    {
      return await _context.Commands.ToListAsync<Command>();
    }

    public async Task<Command> GetCommandById(int id)
    {
      var result = await _context.Commands.FirstOrDefaultAsync(c => c.Id == id);
      return result;
    }
  }
}