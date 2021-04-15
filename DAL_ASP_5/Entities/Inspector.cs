using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class Inspector : IdentityUser
    {
        public int Id { get; set; }
        public string Fullname { get; set; }

    }
}
