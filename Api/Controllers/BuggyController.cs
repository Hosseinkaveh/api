using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class BuggyController:BaseApiController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecet()
        {
            return "secret Text";

        }
         [HttpGet("not-fount")]
        public ActionResult<AppUsers> GetNotFound()
        {
            var thing = _context.users.Find(-1);
            if(thing == null) return NotFound();
            return Ok(thing);

        }
         [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = _context.users.Find(-1);
            var thingsToReturn = thing.ToString();
            return thingsToReturn;

        }
         [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest();

        }
        
    }
}