using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.Models
{
    public class Order
    {
        /// <summary>
        /// Идентификатор заказа
        /// </summary>
        public Guid Id { get; private set; } = Guid.NewGuid();
        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        public Client Client { get; set; }
        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime CreateOrder { get; set; }
    }

}
