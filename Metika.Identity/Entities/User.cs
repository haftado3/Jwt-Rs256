using Microsoft.AspNetCore.Identity;

namespace Metika.Identity.Entities
{
    public class User : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public DateTimeOffset CreatedDate { get; set; }
        //public DateTimeOffset ModifiedDate { get; set; }
        public bool Disabled { get; set; }
    }
}