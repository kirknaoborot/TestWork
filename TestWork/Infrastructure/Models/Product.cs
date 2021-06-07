using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.Models
{
    public class Product
    {
       /// <summary>
       /// Идентификатор товара 
       /// </summary>
       public Guid Id { get;  set; } = Guid.NewGuid();
       /// <summary>
       ///Тип товара 
       /// </summary>
       public Guid ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }
        /// <summary>
        /// Название товара
        /// </summary>
        public string NameProduct { get; set; }
       /// <summary>
       /// Цена товара
       /// </summary>
       public decimal PriceProduct { get; set; }
       /// <summary>
       /// Кол-во товара на складе
       /// </summary>
       public int CountProduct { get; set; }



        


    }
}
