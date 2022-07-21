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

namespace TimeTrackingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimeTrackingSubitemsController : ControllerBase
    {
        private readonly TimeTrackingContext _context;

        public TimeTrackingSubitemsController(TimeTrackingContext context)
        {
            _context = context;
        }

// GET: api/TimeTrackingSubitems
        [HttpGet("{timeTrackingItemId}")]
        public async Task<ActionResult<IEnumerable<TimeTrackingSubitemDTO>>> GetTimeTrackingSubitems(long timeTrackingItemId)
        {
            return await _context.TimeTrackingSubitems
                .Where(si => si.Parent.Id == timeTrackingItemId)
                .Select(si => TimeTrackingSubitemMappers.SubitemToDTO(si))
                .ToListAsync();
           
        }

        // // GET: api/TimeTrackingItem/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<TimeTrackingItemDTOs>> GetTimeTrackingItem(long id)
        // {
        //     var timeTrackingItem = await _context.TimeTrackingItems.FindAsync(id);

        //     if (timeTrackingItem == null)
        //     {
        //         return NotFound();
        //     }

        //     return ItemToDTO(timeTrackingItem);
        // }

        // // PUT: api/TimeTrackingItem/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateTimeTrackingItem(long id, TimeTrackingItemDTOs timeTrackingItemDTO)
        // {
        //     if (id != timeTrackingItemDTO.Id)
        //     {
        //         return BadRequest();
        //     }

        //     // _context.Entry(timeTrackingItem).State = EntityState.Modified;
        //     var timeTrackingItem = await _context.TimeTrackingItems.FindAsync(id);
        //     if(timeTrackingItem == null)
        //     {
        //         return NotFound();
        //     }

        //     timeTrackingItem.Nume = timeTrackingItemDTO.Nume;
        //     timeTrackingItem.Descriere = timeTrackingItemDTO.Descriere;
        //     timeTrackingItem.Data = timeTrackingItemDTO.Data;
        //     timeTrackingItem.NumarOre = timeTrackingItemDTO.NumarOre;
            
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!TimeTrackingItemExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // // POST: api/TimeTrackingItem
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<TimeTrackingItemDTOs>> AddTimeTrackingItem(TimeTrackingItemDTOs timeTrackingItemDTO)
        // {
        //     var timeTrackingItem = new TimeTrackingItem 
        //     {
        //             Nume = timeTrackingItemDTO.Nume,
        //             Descriere = timeTrackingItemDTO.Descriere,
        //             Data = timeTrackingItemDTO.Data,
        //             NumarOre = timeTrackingItemDTO.NumarOre
        //     };
        //     _context.TimeTrackingItems.Add(timeTrackingItem);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction(nameof(GetTimeTrackingItem), new { id = timeTrackingItemDTO.Id }, ItemToDTO(timeTrackingItem));
        // }

        // // DELETE: api/TimeTrackingItem/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteTimeTrackingItem(long id)
        // {
        //     var timeTrackingItem = await _context.TimeTrackingItems.FindAsync(id); 
        //     if (timeTrackingItem == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.TimeTrackingItems.Remove(timeTrackingItem);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // private bool TimeTrackingItemExists(long id)
        // {
        //     return _context.TimeTrackingItems.Any(e => e.Id == id);
        // }


    }

}