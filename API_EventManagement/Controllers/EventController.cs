using API_EventManagement.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_EventManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController(EventAppDbContext eventAppDbContext) : ControllerBase
    {

    }
}
