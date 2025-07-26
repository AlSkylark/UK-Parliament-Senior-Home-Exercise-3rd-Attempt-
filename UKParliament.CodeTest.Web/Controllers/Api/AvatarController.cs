using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Services.Services.Interfaces;

namespace UKParliament.CodeTest.Web.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class AvatarController(IAvatarService service) : ControllerBase
{
    [HttpGet("{seed}")]
    public async Task<FileStreamResult> GetAvatar([FromRoute] string seed)
    {
        return new FileStreamResult(await service.GetAvatar(seed), "image/png");
    }
}
