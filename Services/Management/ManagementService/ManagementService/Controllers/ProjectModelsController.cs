using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManagementService.Models;
using Microsoft.AspNetCore.Authorization;

namespace ManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectModelsController : ControllerBase
    {
        private readonly ProjectContext _context;

        public ProjectModelsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/ProjectModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        // GET: api/ProjectModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectModel>> GetProjectModel(Guid id)
        {
            var projectModel = await _context.Projects.FindAsync(id);

            if (projectModel == null)
            {
                return NotFound();
            }

            return projectModel;
        }

        // PUT: api/ProjectModels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectModel(Guid id, ProjectModel projectModel)
        {
            if (id != projectModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectModelExists(id))
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

        // POST: api/ProjectModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProjectModel>> PostProjectModel(ProjectModel projectModel)
        {
            _context.Projects.Add(projectModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjectModel), new { id = projectModel.Id }, projectModel);
        }

        // DELETE: api/ProjectModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectModel>> DeleteProjectModel(Guid id)
        {
            var projectModel = await _context.Projects.FindAsync(id);
            if (projectModel == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(projectModel);
            await _context.SaveChangesAsync();

            return projectModel;
        }

        private bool ProjectModelExists(Guid id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
