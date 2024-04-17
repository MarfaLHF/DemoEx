using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DemoEx.Models
{
    public class User : IdentityUser
    {
        public string FullnameUser { get; set; }
        public string Role { get; set; }
        
        public virtual ICollection<Application> Applications { get; set; }
    }
}
