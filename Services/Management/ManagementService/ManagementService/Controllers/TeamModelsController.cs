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
    public class TeamModelsController : ControllerBase
    {
        private readonly ManagementContext _context;

        public TeamModelsController(ManagementContext context)
        {
            _context = context;
        }

        // GET: api/TeamModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamModel>>> GetTeams()
        {
            return await _context.Teams.Include(e=>e.EmployeesInTeam).ToListAsync();
        }

        // GET: api/TeamModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamModel>> GetTeamModel(Guid id)
        {
            var teamModel = await _context.Teams.FindAsync(id);

            if (teamModel == null)
            {
                return NotFound();
            }

            return teamModel;
        }

        // PUT: api/TeamModels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeamModel(Guid id, TeamModel teamModel)
        {
            if (id != teamModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(teamModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamModelExists(id))
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

        // POST: api/TeamModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TeamModel>> PostTeamModel(TeamModel teamModel)
        {
            _context.Teams.Add(teamModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeamModel", new { id = teamModel.Id }, teamModel);
        }

        // POST: api/TeamModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("addPeople/{id}")]
        public async Task<ActionResult<TeamModel>> AddPeopleToTeam(Guid id, PeopleModel peopleModel)
        {
            var team = await _context.Teams.FindAsync(id);

            var person = await _context.People.FindAsync(peopleModel.Id);
            if (team.EmployeesInTeam.Contains(person))
            {
                return AcceptedAtAction("AddPeopleToTeam", team);
            }

            team.EmployeesInTeam.Add(person);

            await PutTeamModel(id, team);


            return CreatedAtAction("AddPeopleToTeam", team);
        }

        // DELETE: api/TeamModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TeamModel>> DeleteTeamModel(Guid id)
        {
            var teamModel = await _context.Teams.FindAsync(id);
            if (teamModel == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(teamModel);
            await _context.SaveChangesAsync();

            return teamModel;
        }

        private bool TeamModelExists(Guid id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
