using System;
using System.Collections.Generic;

namespace ihud.WebApi.Models
{
    public partial class Role
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
