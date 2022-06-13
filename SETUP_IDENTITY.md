# Identity and custom user and roles management
* Any web application must have identical way to manage how to add user the base system and how to add roles to every user.
* One of Asp.net features that I can handle the operations on users and the roles of these users (creating, removing, updating ..) using EF core (EntityFramework core).
* The tables that EF core will create them are:
    1. AspNetUsers
    2. AspNetUserClaims
    3. AspNetUserLogins
    4. AspNetUserTokens    
    5. AspNetUserRoles
    6. AspNetRoles
    7. AspNetRolesClaims
> The relational structure of this tables:
> * Users have many of UserClaims, Logins and Tokens.
> * Roles have many of RoleClaims.
> * Users have many of Roles and Roles have many of Users so we need a breaking table and it is UserRoles.

## Adding Identity manually
* You should follow this steps to add Identity feature manually:
1. We need to install these packages:
	1. Microsoft.EntityFrameworkCore.SqlServer
	2. Microsoft.EntityFrameworkCore.Tools
	3. Microsoft.AspNetCore.Identity.EntityFrameworkCore
2. Add foler `Models`
3. Add class and name it `ApplicationUser` and make it  inheritance from class `IdentityUser` and add some properties that are't found in `IdentityUser` class (these properties are associated with user cerdientials like: FirstName, LastName, DateBirth .... etc)
```csharp
 public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        public byte[] ProfilePicture { get; set; }

        ...
    }
```
4. Add folder `Data`.
5. Add class and name it `ApplicationDbConect` and make it inheritance from class `IdentityDbContext<ApplicationUser>` and change the name of identity user tables that EF will create them and **optionally** you can delete some columns that you don't want them from the tables.
> You should put all these tables under **Security** schema.
```csharp
 public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // If you want to delete such column from a table you can use this way (Important note: there is some columns you must not delete them like 'ConcurrencyStamp' and if you do that the table may not work).
            // ex: to delete "PhoneNumber" column from Users table 
            // builder.Entity<ApplicationUser>().Ignore("PhoneNumber");

            builder.Entity<ApplicationUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
        }
    }
```

4. Specify the conenction string that will use to connect to SqlServer database:
```csharp
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\ProjectsV13;Database=UserManagementWebIdentity;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
```

5. We need to add EF core and Identity services to DI (Dependency Injection) in `Startup` class.
```csharp
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        Configuration.GetConnectionString("DefaultConnection")));

services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

```

6. We need to add Authentication and Authorization functionality 
```csharp
app.UseAuthentication();
app.UseAuthorization();
```


6. In the controller All what you want to do is that to declare a fields from `UserManager`, `RoleManager` and `SignInManager` and assign them in the constructor and make DI complete the process and specify the user who can call these Apis using `[Authorize(Roles = "...")]` attribute.
```csharp
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> _signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }
}
``` 

## Adding Identity Automatically using VS wizard
1. First of all you should create Asp.net core app and make Identity **Individual account**.
2. VS will create all Identity fundementals that you want:
	1. Creating `Area` folder and put inside it the .cshtml files (Razor pages) that have Identity process (you aren't force to use this Razor pages you can use your custom Razor Views if you want to use MVC pattern style).
	2. Creating `Data` folder that conatins on migration history and ApplicationDbContext.
    3. Add DbContext and Identity services to DI in `Startup` class.

