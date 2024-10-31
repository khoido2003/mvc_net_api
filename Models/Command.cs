using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
  public class Command
  {
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    [Required]
    public string HowTo { get; set; }

    [Required]
    public string Platform { get; set; }

  }

}