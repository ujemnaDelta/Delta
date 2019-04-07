using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalApp.API.Data;

namespace PortalApp.API.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetValuesAsync()
        {
            List<Models.ValueModel> values = await contextGlobalField.Values.ToListAsync();    // to list, więc widać w teorii mógłby to być var ale tak jest czytelniej(przynajmniej dla mnie. Tymon)

            return Ok(values);
        }

        //DO USUNIĘCIA POTEM
        //[AllowAnonymous]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsersAsync()
        {
            List<Models.UserModel> users = await contextGlobalField.Users.ToListAsync();
            return Ok(users);
        }
        //[AllowAnonymous]
        // GET api/values/5
        [HttpGet("{id}")]
        public  async Task<IActionResult> GetValue(int id)
        {
            //Zwraca pierwszy element sekwencji lub wartość domyślną, jeśli sekwencja nie zawiera żadnych elementów.
            //Lambda zwraca x jeżeli to element który przekazujemy w argumencie.
            var value = await contextGlobalField.Values.FirstOrDefaultAsync(x => x.Id == id);

            return Ok(value);
        }
    }
}
