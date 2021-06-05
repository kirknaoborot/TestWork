using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.Models
{
    public class ProductType
    {
        /// <summary>
        /// Идентификатор типа
        /// </summary>
        public Guid Id { get; private set; } = Guid.NewGuid();
        /// <summary>
        /// Название типа товара
        /// </summary>
        public string NameType { get; set; }

    }
}
