using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        public string Fullname { get; set; }

    }
}
