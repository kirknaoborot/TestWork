using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.Models
{
    public class PositionOrder
    {
        /// <summary>
        /// Идентификатор позиции
        /// </summary>
        public Guid Id { get; private set; } = Guid.NewGuid();
        /// <summary>
        /// Заказ
        /// </summary>
        public Order Order { get; set; }
        /// <summary>
        /// Товар
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Кол-во
        /// </summary>
        public int Count { get; set; }
    }
}
