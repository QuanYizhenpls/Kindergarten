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
    /// Представляет план или мероприятие.
    /// </summary>
    public class Plan
    {
        /// <summary>
        /// Получает или задает уникальный идентификатор плана.
        /// </summary>
        [Key]
        public Guid Plan_Id { get; set; }

        /// <summary>
        /// Получает или задает дату проведения мероприятия.
        /// </summary>
        public DateTime DateOfTheEvent { get; set; }

        /// <summary>
        /// Получает или задает описание мероприятия.
        /// </summary>
        public string Development { get; set; }

        /// <summary>
        /// Получает или задает сотрудника, ответственного за этот план.
        /// </summary>
        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        /// <summary>
        /// Переопределение метода ToString для удобного представления планов в виде текста.
        /// Возвращает описание мероприятия и сотрудника.
        /// </summary>
        /// <returns>Строковое представление плана — описание мероприятия и сотрудник.</returns>
        public override string ToString()
        {
            return $"{DateOfTheEvent} - {Employee}";
        }
    }
}
