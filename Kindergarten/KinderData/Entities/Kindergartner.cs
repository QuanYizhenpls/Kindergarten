using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderData.Entities
{
    /// <summary>
    /// Представляет воспитанника детского сада.
    /// </summary>
    public class Kindergartner
    {
        /// <summary>
        /// Получает или задает уникальный идентификатор воспитанника.
        /// </summary>
        [Key]
        public Guid Kindergartner_Id { get; set; }

        /// <summary>
        /// Получает или задает полное имя воспитанника.
        /// </summary>
        public string FIO { get; set; } 

        /// <summary>
        /// Получает или задает дату рождения воспитанника.
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Получает или задает контактную информацию родителей воспитанника.
        /// </summary>
        public string ParentsContactInfo { get; set; }

        /// <summary>
        /// Получает или задает коллекцию групп, к которым принадлежит сотрудник.
        /// </summary>
        public Guid GroupId { get; set; }
        public Group? Group { get; set; }

        /// <summary>
        /// Переопределение метода ToString для удобного представления воспитанников в виде текста.
        /// Возвращает ФИО, дату рождения и группу.
        /// </summary>
        /// <returns>Строковое представление воспитанника — ФИО, дата рождения и группа.</returns>
        public override string ToString()
        {
            return $"{FIO}, {DateOfBirth}, {ParentsContactInfo} - {Group}";
        }
    }
}
