using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalApp.API.Data;

namespace PortalApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {


        private readonly DataContext contextGlobalField;

        public ValuesController(DataContext context)
        {
            contextGlobalField = context;
        }
        // GET api/values

        [HttpGet]
        public async Task<IActionResult> GetValuesAsync()
        {
            List<Models.ValueModel> values = await contextGlobalField.Values.ToListAsync();    // to list, więc widać w teorii mógłby to być var ale tak jest czytelniej(przynajmniej dla mnie. Tymon)

            return Ok(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public  async Task<IActionResult> GetValue(int id)
        {
            //Zwraca pierwszy element sekwencji lub wartość domyślną, jeśli sekwencja nie zawiera żadnych elementów.
            //Lambda zwraca x jeżeli to element który przekazujemy w argumencie.
            var value = await contextGlobalField.Values.FirstOrDefaultAsync(x => x.Id == id);

            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
