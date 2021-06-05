using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWork.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        ApplicationContext _context;

        public ClientController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("Clients")]
        public IEnumerable<Client> GetClients()
        {
            var clients = _context.Clients;
            return clients;
        }

        [HttpPost("Client")]
        public async Task<IActionResult> PostClient([FromBody] Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetClients), client);
        }
    }
}
