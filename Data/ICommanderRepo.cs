using Commander.Dtos;
using Commander.Models;

namespace Commander.Data
{
  public interface ICommanderRepo
  {
    Task<bool> SaveChanges();

    Task<IEnumerable<Command>> GetAllCommands();
    Task<Command> GetCommandById(int id);
    Task CreateCommand(Command cmd);
    void UpdateCommand(Command cmd);

  }
}

