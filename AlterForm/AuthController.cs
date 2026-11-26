using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlterForm;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase // Or Controller. I forget which one to use
{
    private readonly SignInManager<AlterUser> signin_mgr;
    private readonly UserManager<AlterUser> user_mgr;

    public AuthController(SignInManager<AlterUser> signin, UserManager<AlterUser> usermgr)
    {
        signin_mgr = signin;
        user_mgr = usermgr;
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> logout()
    {
        await signin_mgr.SignOutAsync();
        return Redirect("~/login");
    }

}