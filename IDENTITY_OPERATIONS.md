# Identity operations
* The operations that we can use it on Identity tables are available in `UserManager`, `RoleManager` and `SignInManager` classes
* You can use these classes as defined fields in the Controller
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

## UserManager
* Contains on many of methods that make changes on the Users table and other tables that are connected to it like:
1. Finding a User => FindyByIdAsync(string userId) || FindyByNameAsync(string userId), ....
2. Get specific data from a user => GetPhoneNumberAsync(IdentityUser user) || GetNameAsync(IdentityUser user), ....
3. Adding A new user => CreateAsync(IdentityUser user, string password)
4. Deleting user => DeleteAsync(IdentityUser user)
5. Updating user info => UpdateAsync(IdentityUser user)
6. Adding and Removing  claims of a user => AddClaimsAsync(IdentityUser user, IEnumerable<Claim> claims) || RemoveClaimsAsync(IdentityUser user, IEnumerable<Claim> claims)
7. Generate token for specified user => GenerateUserTokenAsync(IdentityUser user, string tokenProvider, string purpose) || VerifyUserTokenAsync(IdentityUser user, string tokenProvider, string purpose, string token)
8. Adding, removing or getting roles to/of a user => AddToRoleAsync(IdentityUser user, string role) || RemoveFromRoleAsync(TUseIdentityUserr user, string role) || GetRolesAsync(IdentityUser user)

.....


## RoleManager
* Contains on many of methods that make changes on the Roles table and other tables that are connected to it like:
1. Creating role => CreateAsync(IdentityRole role)
2. Deleting role => DeleteAsync(IdentityRole role)
3. Updating role => UpdateAsync(TRole role)
3. Finding a role => FindByIdAsync(string roleId) || FindByNameAsync(string roleName)
4. Getting specific data for a role => GetRoleIdAsync(IdentityRole role) || GetRoleNameAsync(IdentityRole role)


## SignInManager
