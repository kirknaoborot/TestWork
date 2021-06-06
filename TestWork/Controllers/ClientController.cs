using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWork.Models;


namespace TestWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        IMemoryCache _cache;
        ApplicationContext _context;

        public ClientController(ApplicationContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet("Clients")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public async Task<IEnumerable<Client>> GetClients()
        {
            var clients = await _context.Clients.ToListAsync();
            return clients;
        }

        [HttpPost("AddClient")]
        public async Task<IActionResult> AddClient([FromBody] Client client)
        {
            if (client is null)
            {
                throw new Exception(nameof(client));
            }
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetClients), client);
        }
    }
}
