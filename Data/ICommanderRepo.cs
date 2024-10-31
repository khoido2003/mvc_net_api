using Commander.Models;

namespace Commander.Data
{
  public interface ICommanderRepo
  {
    Task<IEnumerable<Command>> GetAllCommands();
    Task<Command> GetCommandById(int id);
  }
}

