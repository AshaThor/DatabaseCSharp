using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgresConnect.Data;
using PostgresConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgresConnect.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        public PizzaController(AshsDbContext ashsDbContext)
        {
            AshsDbContext = ashsDbContext;
        }

        public AshsDbContext AshsDbContext { get; }

        [HttpGet]
        public async Task<ActionResult<Pizza>> Get()
        {
            Pizza pizza = await AshsDbContext.Pizzas.FirstOrDefaultAsync();
            return pizza;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Pizza model)
        {
            if (model is null)
                return NotFound();
            try
            {
                await AshsDbContext.AddAsync(model);
                await AshsDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
            return NoContent();
        }
    }
}
