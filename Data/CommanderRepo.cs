using Commander.Dtos;
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

    //////////////////////////////////

    public async Task<bool> SaveChanges()
    {
      var r = await _context.SaveChangesAsync();
      return r > 0;
    }


    ////////////////////////////////////

    public async Task CreateCommand(Command cmd)
    {
      ArgumentNullException.ThrowIfNull(cmd);

      await _context.Commands.AddAsync(cmd);
    }

    ////////////////////////////////////////

    public async Task<IEnumerable<Command>> GetAllCommands()
    {
      return await _context.Commands.ToListAsync<Command>();
    }

    ////////////////////////////////////

    public async Task<Command> GetCommandById(int id)
    {
      var result = await _context.Commands.FirstOrDefaultAsync(c => c.Id == id);
      return result;
    }

    ////////////////////////////////////

    public void UpdateCommand(Command cmd)
    {

    }


  }
}
