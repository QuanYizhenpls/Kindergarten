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
        public DateOnly DateOfTheEvent { get; set; }

        /// <summary>
        /// Получает или задает описание мероприятия.
        /// </summary>
        public string? Development { get; set; }

        /// <summary>
        /// Получает или задает группу, для которой предназначен этот план.
        /// </summary>
        [ForeignKey("Group_Id")]
        public virtual Group Group { get; set; } = null!;

        /// <summary>
        /// Получает или задает сотрудника, ответственного за этот план.
        /// </summary>
        [ForeignKey("Employee_Id")]
        public virtual Employee Employee { get; set; } = null!;
    }
}
