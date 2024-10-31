using Commander.Models;

namespace Commander.Data
{
  public class MockCommanderRepo : ICommanderRepo
  {

    public Task<IEnumerable<Command>> GetAllCommands()
    {

      var commands = new List<Command> {
        new() {
        Id = 0,
        HowTo = "Make a cup of coffee",
        Platform = "Mac, Windows, Linux"
      }, new ()
      {
        Id = 1,
        HowTo = "Make a cup of coffee 1",
        Platform = "Mac, Windows, Linux"
      },
       new ()      {
        Id = 2,
        HowTo = "Make a cup of coffee 2",
        Platform = "Mac, Windows, Linux"
      }
      };
      return Task.Run(() =>
      {
        return commands.AsEnumerable();
      });

    }

    public Task<Command> GetCommandById(int id)
    {

      return Task.FromResult(new Command
      {
        Id = 0,
        HowTo = "Make a cup of coffee",
        Platform = "Mac, Windows, Linux"
      });

    }
  }
}
