using Metika.Identity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Jwt_Rs256.Persistance
{
    public class ApplicationContext : UserDbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
