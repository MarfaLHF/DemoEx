using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;

namespace DemoEx.Models
{
    public class TypeProblem
    {
        [Key]
        public int IdTypeProblem { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
    }
}
