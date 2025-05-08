using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderData.Entities
{
    /// <summary>
    /// Представляет воспитанника детского сада.
    /// </summary>
    public class Kindergartner
    {
        /// <summary>
        /// Получает или задает уникальный идентификатор воспитанника.
        /// </summary>
   
        public Guid Kindergartner_Id { get; set; }

        /// <summary>
        /// Получает или задает полное имя воспитанника.
        /// </summary>
        public string? FIO { get; set; } 

        /// <summary>
        /// Получает или задает дату рождения воспитанника.
        /// </summary>
        public string? DateOfBirth { get; set; }

        /// <summary>
        /// Получает или задает контактную информацию родителей воспитанника.
        /// </summary>
        public string? ParentsContactInfo { get; set; }
    }
}
