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
    /// Представляет данные о сотруднике.
    /// </summary>
    public class EmployeeData
    {
        /// <summary>
        /// Получает или задает идентификатор сотрудника.
        /// </summary>
       
        public Guid EmployeeData_Id { get; set; }

        /// <summary>
        /// Получает или задает номер паспорта сотрудника.
        /// </summary>
        public int Pasport { get; set; }

        /// <summary>
        /// Получает или задает СНИЛС сотрудника.
        /// </summary>
        public int SNILS { get; set; }

        /// <summary>
        /// Получает или задает ИНН сотрудника.
        /// </summary>
        public int INN { get; set; }

        /// <summary>
        /// Получает или задает информацию о трудовой книжке сотрудника.
        /// </summary>
        public string? EmploymentRecord { get; set; }


        /// <summary>
        /// Получает или задает коллекцию сотрудников, связанных с этими данными.
        /// </summary>
        [ForeignKey("Employee_Id")]
        public Employee Employees { get; set; }

    }
}
