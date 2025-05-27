using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        [Key]
        public Guid EmployeeData_Id { get; set; }

        /// <summary>
        /// Получает или задает номер паспорта сотрудника.
        /// </summary>
        public string Pasport { get; set; } 

        /// <summary>
        /// Получает или задает СНИЛС сотрудника.
        /// </summary>
        public string SNILS { get; set; }

        /// <summary>
        /// Получает или задает ИНН сотрудника.
        /// </summary>
        public string INN { get; set; }

        /// <summary>
        /// Получает или задает информацию о трудовой книжке сотрудника.
        /// </summary>
        public string EmploymentRecord { get; set; }


        /// <summary>
        /// Получает или задает коллекцию сотрудников, связанных с этими данными.
        /// </summary>
        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        /// <summary>
        /// Переопределение метода ToString для удобного представления данных сотрудников в виде текста.
        /// Возвращает паспорт и сотрудника.
        /// </summary>
        /// <returns>Строковое представление данных сотрудника — паспорт и сотрудник.</returns>
        public override string ToString()
        {
            return $"{Pasport} - {Employee}";
        }
    }
}
