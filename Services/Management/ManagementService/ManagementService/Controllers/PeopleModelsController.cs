using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManagementService.Models;

namespace ManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleModelsController : ControllerBase
    {
        private readonly PeopleContext _context;

        public PeopleModelsController(PeopleContext context)
        {
            _context = context;
        }

        // GET: api/PeopleModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeopleModel>>> GetPeople()
        {
            return await _context.People.ToListAsync();
        }

        // GET: api/PeopleModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PeopleModel>> GetPeopleModel(Guid id)
        {
            var peopleModel = await _context.People.FindAsync(id);

            if (peopleModel == null)
            {
                return NotFound();
            }

            return peopleModel;
        }

        // PUT: api/PeopleModels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeopleModel(Guid id, PeopleModel peopleModel)
        {
            if (id != peopleModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(peopleModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeopleModelExists(id))
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

        // POST: api/PeopleModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PeopleModel>> PostPeopleModel(PeopleModel peopleModel)
        {
            _context.People.Add(peopleModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPeopleModel", new { id = peopleModel.Id }, peopleModel);
        }

        // DELETE: api/PeopleModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PeopleModel>> DeletePeopleModel(Guid id)
        {
            var peopleModel = await _context.People.FindAsync(id);
            if (peopleModel == null)
            {
                return NotFound();
            }

            _context.People.Remove(peopleModel);
            await _context.SaveChangesAsync();

            return peopleModel;
        }

        private bool PeopleModelExists(Guid id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
