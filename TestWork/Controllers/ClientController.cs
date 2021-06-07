using Microsoft.AspNetCore.Authorization;
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
    /// <summary>
    /// Контроллер работы с клиентами
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        ApplicationContext _context;
        /// <summary>
        /// Контроллер работы с клиентами
        /// </summary>
        public ClientController(ApplicationContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Метод получения списка клиентов
        /// </summary>
        /// <returns>Список клиентов</returns>
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 100)]
        public async Task<IEnumerable<Client>> Get()
        {
            var clients = await _context.Clients.ToListAsync();
            return clients;
        }
        /// <summary>
        /// Метод добавления клиента
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Client client)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), client);
        }
    }
}
