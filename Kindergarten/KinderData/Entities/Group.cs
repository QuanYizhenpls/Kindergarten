using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderData.Entities
{
    /// <summary>
    /// Представляет группу.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Получает или задает уникальный идентификатор группы.
        /// </summary>
        [Key]
        public Guid Group_Id { get; set; }

        /// <summary>
        /// Получает или задает наименование группы.
        /// </summary>
        public string? GroupName { get; set; }

        
        /// <summary>
        /// Получает или задает коллекцию детей, входящих в эту группу.
        /// </summary>
        public Kindergartner? Kindergartners { get; set; } = null!;

       
    }
}
