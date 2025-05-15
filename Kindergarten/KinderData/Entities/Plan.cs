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
        public string? DateOfTheEvent { get; set; }

        /// <summary>
        /// Получает или задает описание мероприятия.
        /// </summary>
        public string? Development { get; set; }

        /// <summary>
        /// Получает или задает группу, для которой предназначен этот план.
        /// </summary>
        //public Guid GroupId { get; set; }
        //public Group? Group { get; set; }

        /// <summary>
        /// Получает или задает сотрудника, ответственного за этот план.
        /// </summary>
        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public override string ToString()
        {
            return $"{Development} - {Employee}";
        }
    }
}
