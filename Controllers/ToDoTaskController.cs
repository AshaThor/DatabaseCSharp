using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PostgresConnect.Data;
using PostgresConnect.Models;

namespace PostgresConnect.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoTaskController : ControllerBase
    {
        public ToDoTaskController(AshsDbContext ashsDbContext)
        {
            AshsDbContext = ashsDbContext;
        }

        public AshsDbContext AshsDbContext { get; }

        [HttpGet]
        public async Task<ActionResult<ToDoTask>> Get()
        {
            ToDoTask toDoTask = await AshsDbContext.ToDoTasks.FirstOrDefaultAsync();
            if (toDoTask is null)
                return NotFound();
            return toDoTask;
        }

        //TODO get this working
        /*[HttpGet("{id}")]
        public async Task<ActionResult<ToDoTask>> Get(int id)
        {
            //
            //ToDoTask toDoTask = await AshsDbContext.ToDoTasks.Where(t => t.id == id).Single();
            if (toDoTask is null)
                return NotFound();
            return toDoTask;
        }*/

        [HttpPost]
        public async Task<IActionResult> Post(ToDoTask model)
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
