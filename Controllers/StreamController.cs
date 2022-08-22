using Microsoft.AspNetCore.Mvc;

namespace Stremio.Net.Controllers;

[ApiController]
[Route("[controller]")]
[Route("{any:regex(^.*$)}/[controller]")]
public class StreamController : ControllerBase
{
    
}