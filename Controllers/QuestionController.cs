using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PostgresConnect.Data;
using PostgresConnect.Models;

namespace PostgresConnect.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        public QuestionController(AshsDbContext ashsDbContext)
        {
            AshsDbContext = ashsDbContext;
        }

        public AshsDbContext AshsDbContext { get; }

        [HttpGet]
        public async Task<ActionResult<List<Question>>> Get()
        {
            List<Question> questions = await AshsDbContext.Question.ToListAsync();
            if (questions is null)
                return NotFound();
            return questions;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> Get(int id)
        {
            Question question = await AshsDbContext.Question.SingleOrDefaultAsync(t => t.id == id);
            if (question is null)
                return NotFound();
            return question;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Question model)
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
