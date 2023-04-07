using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesOrder.Data;
using SalesOrder.Models;

namespace SalesOrder.Controllers
{
    [Authorize]//[Authorize(Roles = "Admin")] // Only give access to the Admin role
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : BaseController
    {
        public EntriesController(SalesOrderDbContext ctx, IConfiguration iConfig) : base(ctx, iConfig)
        {
        }

        // GET: api/salesOrderLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesOrderLines>>> GetSalesOrderLiness()
        {
          if (context.SalesOrderLines == null)
          {
              return NotFound();
          }
            return await context.SalesOrderLines.ToListAsync();
        }

        // GET: api/salesOrderLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesOrderLines>> GetSalesOrderLine(int id)
        {
          if (context.SalesOrderLines == null)
          {
              return NotFound();
          }
            var orderLine = await context.SalesOrderLines.FindAsync(id);

            if (orderLine == null)
            {
                return NotFound();
            }

            return orderLine;
        }

        // PUT: api/salesOrderLines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesOrderLine(int id, SalesOrderLines entry)
        {
            if (id != entry.Id)
            {
                return BadRequest();
            }
            context.Entry(entry).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id))
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


        // DELETE: api/salesOrderLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesOrderLine(int id)
        {
            if (context.Entries == null)
            {
                return NotFound();
            }
            var entry = await context.SalesOrderLines.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            context.SalesOrderLines.Remove(entry);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntryExists(int id)
        {
            return (context.SalesOrderLines?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
