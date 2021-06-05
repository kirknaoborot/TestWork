using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.Models
{
    public class Client
    {
        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        public Guid Id { get; private set;} = Guid.NewGuid();
        /// <summary>
        /// Имя клиента
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }
    }
}
