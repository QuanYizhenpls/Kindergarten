using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace KinderData.Entities
{
    /// <summary>
    /// Представляет собой договоры, связанные с сотрудником.
    /// </summary>
    public class Agreement
    {
        /// <summary>
        /// Получает или задает уникальный идентификатор договора.
        /// </summary>
        [Key]
        public Guid Agreement_Id { get; set; }

        /// <summary>
        /// Получает или задает информацию о договоре по отпуску.
        /// </summary>
        public string? Vacation { get; set; }

        /// <summary>
        /// Получает или задает информацию о договоре по больничному.
        /// </summary>
        public string? SickLeave { get; set; }

        /// <summary>
        /// Получает или задает информацию о договоре при увольнении.
        /// </summary>
        public string? Dismissal { get; set; }

        /// <summary>
        /// Получает или задает информацию по трудовому договору.
        /// </summary>
        public string? EmploymentContract { get; set; }

        /// <summary>
        /// Получает или задает сотрудника, с которым связан это договор.
        /// </summary>
        public Employee? Employees { get; set; }
        
    }
}
