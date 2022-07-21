#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTrackingApi.Models;
using TimeTrackingApi.DTOs;
using TimeTrackingApi.Mappers;
using System.Security.Claims;

namespace TimeTrackingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimeTrackingItemsController : ControllerBase
    {
        private readonly TimeTrackingContext _context;

        public TimeTrackingItemsController(TimeTrackingContext context)
        {
            _context = context;
        }

// GET: api/TimeTrackingItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeTrackingItemDTO>>> GetTimeTrackingItems(DateTime? searchData, int? searchNumarOre)
        {
            var claims = (User.Identity as ClaimsIdentity).Claims;
            var currentLoggedInUser =_context.Users.Find(long.Parse(claims.FirstOrDefault(c => c.Type == "UserId").Value));
            var query = _context.TimeTrackingItems.AsQueryable();

            if(searchData != null)
            {
                query = query.Where(item =>(item.Data == searchData));
            } 
            else if (searchNumarOre != null)
            {
                query = query.Where(item =>(item.NumarOre == searchNumarOre));
            }
            return await query.Include(t => t.TimeTrackingSubitems)
                .Select(item => TimeTrackingItemMappers.ItemToDTO(item)).ToListAsync();
           
        }

        // GET: api/TimeTrackingItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TimeTrackingItemDTO>> GetTimeTrackingItem(long id)
        {
            var timeTrackingItem = await _context.TimeTrackingItems.FindAsync(id);

            if (timeTrackingItem == null)
            {
                return NotFound();
            }

            return TimeTrackingItemMappers.ItemToDTO(timeTrackingItem);
        }

        // PUT: api/TimeTrackingItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeTrackingItem(long id, TimeTrackingItemDTO timeTrackingItemDTO)
        {
            if (id != timeTrackingItemDTO.Id)
            {
                return BadRequest();
            }

            // _context.Entry(timeTrackingItem).State = EntityState.Modified;
            var timeTrackingItem = await _context.TimeTrackingItems.FindAsync(id);
            if(timeTrackingItem == null)
            {
                return NotFound();
            }

            timeTrackingItem.Nume = timeTrackingItemDTO.Nume;
            timeTrackingItem.Descriere = timeTrackingItemDTO.Descriere;
            timeTrackingItem.Data = timeTrackingItemDTO.Data;
            timeTrackingItem.NumarOre = timeTrackingItemDTO.NumarOre;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeTrackingItemExists(id))
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

        // POST: api/TimeTrackingItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TimeTrackingItemDTO>> AddTimeTrackingItem(TimeTrackingItemDTO timeTrackingItemDTO)
        {
            var currentLoggedInUser = User.Identity.Name;
            var timeTrackingItem = TimeTrackingItemMappers.DTOToItem(timeTrackingItemDTO);
    
            _context.TimeTrackingItems.Add(timeTrackingItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTimeTrackingItem), new { id = timeTrackingItemDTO.Id }, TimeTrackingItemMappers.ItemToDTO(timeTrackingItem));
        }

        // DELETE: api/TimeTrackingItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeTrackingItem(long id)
        {
            var timeTrackingItem = await _context.TimeTrackingItems.FindAsync(id); 
            if (timeTrackingItem == null)
            {
                return NotFound();
            }

            _context.TimeTrackingItems.Remove(timeTrackingItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TimeTrackingItemExists(long id)
        {
            return _context.TimeTrackingItems.Any(e => e.Id == id);
        }
        

    }

}