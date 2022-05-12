using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mysys.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string RoleId { get; set; }
        [ForeignKey("RoleId")]

        public virtual Role Role { get; set; }
        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public bool Status { get; set; }
    }
}
