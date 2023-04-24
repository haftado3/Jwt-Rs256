### How to use
this project dependes on Metika.Security project.

1. first you need to add connection string to appsettings :
```c#
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOURSERVERNAME; Database=YOURDATABASENAME; Trusted_Connection=True; MultipleActiveResultSets=true"
  },
  "SecurityOptions": {
    "PrivateKeyFilePath": "keys/private.xml",
    "PublicKeyFilePath": "keys/public.xml"
  }
```
dont forget to remove Security Options from appseting on production and move to User secrets or a safe vault.

2. you need to create your DbContext like this :
```c#
   using Metika.Identity.Context;

    public class ApplicationContext : UserDbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            
        }
    }
```
3. you need to install you database for ef core :

- using dotnet cli :
```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.5
```

- using package manager :

```
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 7.0.5
```

4. write this line of code inside your program.cs file depending on your database options part could be different:
```c#
    using Metika.Identity.Context;

    builder.Services.AddIdentityContext<ApplicationContext>(builder.Configuration, 
        options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        }
    );
```

5. and the for security reasons and also authentication and authorization you need this code at your pipeline :
```c#
    using Metika.Identity.Context;

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseIdentityContext();
```
6. then you need to create a controller that inherits from base user controller
```c#
[Route("api/[controller]")]
[ApiController]
public class UserController : BaseUserController<User,Role>
{
    private readonly IUserService<User, Role> _userService;

    public UserController(IUserService<User, Role> userService) : base(userService)
    {
        _userService = userService;
    }
    [Authorize]
    [HttpGet("Validate")]
    public async Task<IActionResult> Validate()
    {
        return Ok();
    }
}
```

7. adding migration to database 
```
dotnet ef migrations add InitialMigration
```
8. update database
```
dotnet ef database update
```