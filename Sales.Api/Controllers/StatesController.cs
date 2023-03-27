using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.Api.Data;
using Sales.Api.Helpers;
using Sales.Shared.DTOs;
using Sales.Shared.Entities;

namespace Sales.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/states")]
    public class StatesController : ControllerBase
    {
        private readonly DataContext _context;

        public StatesController(DataContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("combo/{countryId:int}")]
        public async Task<ActionResult> GetCombo(int countryId)
        {
            return Ok(await _context.States
                .Where(x => x.CountryId == countryId)
                .ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.States.
                Include(x => x.Cities)
                .Where(x => x.Country!.Id == pagination.Id).AsQueryable();

            if (!string.IsNullOrEmpty(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<IActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.States
                .Where(x => x.Country!.Id == pagination.Id)
                .AsQueryable();
            if (!string.IsNullOrEmpty(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var states = await _context.States
                .Include(x => x.Cities)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (states == null)
            {
                return NotFound();
            }

            return Ok(states);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(State state)
        {
            try
            {
                _context.Add(state);
                await _context.SaveChangesAsync();
                return Ok(state);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un estado con el mismo nombre");
                }

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(State state)
        {
            try
            {
                _context.Update(state);
                await _context.SaveChangesAsync();
                return Ok(state);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un estado con el mismo nombre");
                }

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var state = await _context.States.FirstOrDefaultAsync(x => x.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            _context.Remove(state);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}