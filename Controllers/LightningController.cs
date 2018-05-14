using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BitarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LightningController : ControllerBase
    {
        // GET lightning
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET lightning/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST lightning
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT lightning/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE lightning/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}