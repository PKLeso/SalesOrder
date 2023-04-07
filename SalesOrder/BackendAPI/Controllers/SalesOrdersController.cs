using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesOrder.Data;
using SalesOrder.Models;

namespace SalesOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrdersController : BaseController
    {
        public SalesOrdersController(SalesOrderDbContext ctx, IConfiguration iConfig) : base(ctx, iConfig)
        {
        }

        // GET: api/salesOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesOrders>>> GetSalesOrders()
        {
            if (context.SalesOrders == null)
            {
                return NotFound();
            }
            return await context.SalesOrders.ToListAsync();
        }

        // GET: api/salesOrder/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesOrders>> GetSalesOrder(int id)
        {
            if (context.SalesOrders == null)
            {
                return NotFound();
            }
            var order = await context.SalesOrders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/salesOrder/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesOrder(int id, SalesOrders order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }
            order.UpdatedOn = DateTime.Now;
            context.Entry(order).State = EntityState.Modified;

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

        // POST: api/salesOrders
        [HttpPost]
        public async Task<ActionResult<SalesOrders>> PostEntry(SalesOrders order)
        {
            if (context.Entries == null)
            {
                return Problem("Entity set 'PhonebookDbContext.Entries'  is null.");
            }
            order.CreatedOn = DateTime.Now;
            context.SalesOrders.Add(order);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetSalesOrder", new { id = order.Id }, order);
        }

        // DELETE: api/salesOrder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesOrder(int id)
        {
            if (context.SalesOrders == null)
            {
                return NotFound();
            }
            var order = await context.SalesOrders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            context.SalesOrders.Remove(order);
            await context.SaveChangesAsync();

            return NoContent();
        }


        private bool EntryExists(int id)
        {
            return (context.SalesOrders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
