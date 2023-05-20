using Hack.Data;
using Hack.Models;
using Hack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Hack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CashController : Controller
    {
        Guid id = new Guid("9bf50d22-07b6-49e1-8dd9-90aed556ddb0");

        private CashRequst jsonRequst = new CashRequst();
        private readonly DbContextHack _context;
        public CashController(DbContextHack context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCash()
        {
            var history = await _context.HistoryEvents.ToListAsync();
            var balances = await _context.UserCashStates.Select(u => u.Balance).ToListAsync();
            float balance = balances[0];

            var jsonRequest = new CashRequst
            {
                Balance = balance,
                CashEvents = history
            };

            string jsonRequ = JsonConvert.SerializeObject(jsonRequest);

            return Ok(jsonRequ);
        }

        [HttpPut]
        public async Task<IActionResult> PushCash([FromBody] HistoryEvent _historyEvent)
        {
            var balanceupdate = await _context.UserCashStates.FindAsync(id);

            _historyEvent.Id = Guid.NewGuid();

            if (balanceupdate == null)
            {
                return NotFound();
            }

            if (_historyEvent.Type == "DEPOSIT")
            {
               balanceupdate.Balance += _historyEvent.Amount;
            }
            else if(_historyEvent.Type == "WITHDRAW") 
            {
                balanceupdate.Balance -= _historyEvent.Amount;
                if (balanceupdate.Balance < 0)
                {
                    return Ok("Error");
                }
            }

            await _context.HistoryEvents.AddAsync(_historyEvent);
            await _context.SaveChangesAsync();

            return Ok(_historyEvent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoryEvent(Guid id)
        {
            var historyEvent = await _context.HistoryEvents.FindAsync(id);

            if (historyEvent == null)
            {
                return NotFound();
            }

            _context.HistoryEvents.Remove(historyEvent);
            await _context.SaveChangesAsync();

            return Ok(historyEvent);
        }

    }
}
