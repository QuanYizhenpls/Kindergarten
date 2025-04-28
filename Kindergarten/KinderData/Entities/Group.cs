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
        public Guid Groups_Id { get; set; }

        /// <summary>
        /// Получает или задает наименование группы.
        /// </summary>
        public string? GroupsName { get; set; }

        /// <summary>
        /// Получает или задает коллекцию детей, входящих в эту группу.
        /// </summary>
        public virtual ICollection<Kindergartner> Kindergartners { get; set; } = null!;

        /// <summary>
        /// Получает или задает воспитанника, связанного с группой.
        /// </summary>
        [ForeignKey("Kindergartner_Id")]
        public virtual Kindergartner Kindergartner { get; set; } = null!;
    }
}
