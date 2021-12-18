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
    public class AshController : ControllerBase
    {
        public AshController(AshsDbContext ashsDbContext)
        {
            AshsDbContext = ashsDbContext;
        }

        public AshsDbContext AshsDbContext { get; }



        [HttpGet]
        public async Task<ActionResult<Ash>> Get()
        {
            Ash ash = await AshsDbContext.Ashs.FirstOrDefaultAsync();
            return ash;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Ash model)
        {
            try
            {
                await AshsDbContext.AddAsync(model);
                await AshsDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e);
            }

            return Ok();
        }
    }
}
