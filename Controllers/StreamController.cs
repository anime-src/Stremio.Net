using Microsoft.AspNetCore.Mvc;

namespace Stremio.Net.Controllers
{
    // R: {*addon}.domain.com/stream
// R: domain.com/{*addon}/stream
    [ApiController]
    [Route("[controller]")]
    [Route("{any:regex(^.*$)}/[controller]")]
    public class StreamController : ControllerBase
    {
    
    }
}