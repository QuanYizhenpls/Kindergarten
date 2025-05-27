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
    /// Представляет сотрудника.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Получает или задает уникальный идентификатор сотрудника.
        /// </summary>
        [Key]
        public Guid Employee_Id { get; set; }

        /// <summary>
        /// Получает или задает полное имя сотрудника.
        /// </summary>
        public string FIO { get; set; }

        /// <summary>
        /// Получает или задает образование сотрудника.
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        /// Получает или задает опыт работы сотрудника.
        /// </summary>
        public string Experience { get; set; }

        /// <summary>
        /// Получает или задает должность сотрудника.
        /// </summary>
        public string Post { get; set; }

        /// <summary>
        /// Получает или задает коллекцию групп, к которым принадлежит сотрудник.
        /// </summary>
        public Guid GroupId { get; set; }
        public Group? Group { get; set; }

        /// <summary>
        /// Переопределение метода ToString для удобного представления сотрудников в виде текста.
        /// Возвращает ФИО и группу.
        /// </summary>
        /// <returns>Строковое представление сотрудника —  ФИО и группа.</returns>
        public override string ToString()
        {
            return $"{FIO} - {Group}";
        }
    }
}
