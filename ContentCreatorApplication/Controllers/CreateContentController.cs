using ContentCreatorApplication.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContentCreatorApplication.Controllers
{

    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class CreateContentController : ControllerBase
    {
      
        public CreateContentController()
        {
                string ContentCreatorApplication= null;      
        }
        // GET: api/<CreateContentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CreateContentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CreateContentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CreateContentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CreateContentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
