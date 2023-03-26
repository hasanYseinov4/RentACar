using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{

    public class User : IdentityUser<string>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EGN { get; set; }
        public bool IsAdimin { get; set; }
        public IEnumerable<Request> Requests { get; set; }
    }
}
