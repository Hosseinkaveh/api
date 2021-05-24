using System;
using System.Collections.Generic;

using Api.Extension;

namespace Api.Entities
{
    public class AppUsers
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public DateTime DateOfBirth {get;set;}

        public string KnownAs { get; set; }

        public DateTime Create { get; set; }=DateTime.Now;

        public DateTime LastActive { get; set; }

        public string Gender { get; set; }

        public string Interduction { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }
        public string City { get; set; }

        public string Country { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public ICollection<UserLike> LikedByUsers { get; set; }
        public ICollection<UserLike> LikedUsers { get; set; }
    }
}