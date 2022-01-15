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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostgresConnect.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardController : ControllerBase
    {
        public AshsDbContext AshsDbContext { get; }
        public BoardController(AshsDbContext ashsDbContext)
        {
        AshsDbContext = ashsDbContext;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<List<Board>>> Get()
        {
            List<Board> boards = await AshsDbContext.Board.ToListAsync();
            if (boards is null)
                return NotFound();
            return boards;
            ;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Board>> Get(int id)
        {
            Board board = await AshsDbContext.Board.SingleOrDefaultAsync(t => t.id == id);
            if (board is null)
                return NotFound();
            return board;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
