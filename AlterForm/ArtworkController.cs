using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlterForm;

[ApiController]
[Route("api/[controller]")]
public class ArtworkController : Controller
{
    private readonly AlterDB db;

    public ArtworkController(AlterDB _db) => db =_db;

    [Authorize]
    [ValidateAntiForgeryToken]
    [HttpPost("upload")]
    public async Task<IActionResult> upload([FromForm] ArtworkDTO metadata, FormFile artwork)
    {
        return Ok();
    }


}