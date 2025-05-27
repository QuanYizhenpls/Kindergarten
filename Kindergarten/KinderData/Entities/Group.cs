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
        public string GroupName { get; set; }

        /// <summary>
        /// Переопределение метода ToString для удобного представления групп в виде текста.
        /// Возвращает название группы.
        /// </summary>
        /// <returns>Строковое представление группы — название группы.</returns>
        public override string ToString()
        {
            return $"{GroupName} ";
        }
    }
}
