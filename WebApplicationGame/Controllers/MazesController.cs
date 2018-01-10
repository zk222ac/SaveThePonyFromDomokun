using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationGame.Model;

namespace WebApplicationGame.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MazesController : Controller
    {
        private readonly MazeContext _context;

        public MazesController(MazeContext context)
        {
            _context = context;
        }

        // GET: api/Mazes
        [HttpGet]
        public IEnumerable<Maze> GetMazeDb()
        {
            return _context.Maze.ToList();
        }

        // GET: api/Mazes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaze([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maze = await _context.Maze.SingleOrDefaultAsync(m => m.Id == int.Parse(id));

            if (maze == null)
            {
                return NotFound();
            }

            return Ok(maze);
        }

        // PUT: api/Mazes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaze([FromRoute] string id, [FromBody] Maze maze)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (int.Parse(id) != maze.Id)
            {
                return BadRequest();
            }

            _context.Entry(maze).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MazeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Mazes
        [HttpPost]
        public async Task<IActionResult> PostMaze([FromBody] Maze maze)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Maze.Add(maze);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaze", new { id = maze.Id }, maze);
        }

        // DELETE: api/Mazes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaze([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maze = await _context.Maze.SingleOrDefaultAsync(m => m.Id ==int.Parse(id));
            if (maze == null)
            {
                return NotFound();
            }

            _context.Maze.Remove(maze);
            await _context.SaveChangesAsync();

            return Ok(maze);
        }

        private bool MazeExists(string id)
        {
            return _context.Maze.Any(e => e.Id == int.Parse(id));
        }
    }
}