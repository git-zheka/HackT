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
    public class CryptoController : Controller
    {
        private CryptoRequst jsonRequst = new CryptoRequst();
        private readonly DbContextHack _context;
        public CryptoController(DbContextHack context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCrypto() 
        {
            var history = await _context.HistoryCryptoEvents.ToListAsync();
            var balances = await _context.UserCryptoStates.ToListAsync();

            var jsonRequest = new CryptoRequst
            {
                Balance = balances,
                HistoryCryptoEvents = history
            };

            string jsonRequ = JsonConvert.SerializeObject(jsonRequest);

            return Ok(jsonRequ);
        }

        [HttpPut]
        public async Task<IActionResult> PushCrypto(int _idCoin, [FromBody] HistoryCryptoEvent _historyEvent)
        {
            var balanceUpdate = await _context.UserCryptoStates.FirstOrDefaultAsync(u => u.IdCoinIT == _idCoin);


            if (balanceUpdate == null)
            {
                var newCoin = new UserCryptoState() { IdCoinIT = _idCoin, Balance = _historyEvent.Amount };
                await _context.UserCryptoStates.AddAsync(newCoin);
            }
            else
            {
                balanceUpdate.Balance += _historyEvent.Amount;
            }
            
            await _context.HistoryCryptoEvents.AddAsync(_historyEvent);
            await _context.SaveChangesAsync();

            return Ok(_historyEvent);
        }

    }
}
