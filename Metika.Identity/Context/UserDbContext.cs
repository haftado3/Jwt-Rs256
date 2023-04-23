using Metika.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Metika.Identity.Context
{
    public class UserDbContext : IdentityDbContext<User, Role, long>
    {

    }
}