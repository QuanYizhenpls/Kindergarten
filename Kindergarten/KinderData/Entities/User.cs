using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderData.Entities
{
    /// <summary>
    /// Класс, представляющий пользователя системы.
    /// Содержит свойства для хранения личных данных и учетных данных пользователя.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// Используется для однозначной идентификации записи в базе данных.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// Строка не может быть null (указывается с помощью оператора null-forgiving '!').
        /// </summary>
        public string Firstname { get; set; } = null!;

        /// <summary>
        /// Фамилия пользователя.
        /// Строка не может быть null.
        /// </summary>
        public string Lastname { get; set; } = null!;

        /// <summary>
        /// Отчество пользователя.
        /// Может быть null, если отчество отсутствует.
        /// </summary>
        public string? Middlename { get; set; } = null!;

        /// <summary>
        /// Логин пользователя для аутентификации.
        /// Не может быть null.
        /// </summary>
        public string Login { get; set; } = null!;

        /// <summary>
        /// Пароль пользователя.
        /// Не может быть null.
        /// Хранится в виде строки (желательно хранить в защищённом виде, например, хешировть).
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// Полное имя пользователя, вычисляемое свойство.
        /// Формируется как "Фамилия Имя Отчество", если есть отчество,
        /// либо просто "Фамилия Имя", если отчество отсутствует.
        /// Атрибут <see cref="NotMapped"/> указывает на то, что это свойство не сохраняется в базе данных.
        /// </summary>
        [NotMapped]
        public string Fullname
        {
            get
            {
                // Проверяем есть ли отчество
                if (Middlename != null)
                {
                    // Возвращаем строку с фамилией, именем и отчеством через пробелы
                    return $"{Lastname} {Firstname} {Middlename}";
                }
                else
                {
                    // Возвращаем строку с фамилией и именем, если отчества нет
                    return $"{Lastname} {Firstname}";
                }
            }
        }

        /// <summary>
        /// Переопределение метода ToString для удобного представления пользователя в виде текста.
        /// Возвращает полное имя пользователя.
        /// </summary>
        /// <returns>Строковое представление пользователя — полное имя.</returns>
        public override string ToString()
        {
            return $"{Fullname} ";
        }
    }
}
