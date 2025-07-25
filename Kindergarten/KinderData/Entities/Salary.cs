﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KinderData.Entities
{
    /// <summary>
    /// Представляет информацию о заработной плате сотрудника.
    /// </summary>
    public class Salary
    {
        /// <summary>
        /// Получает или задает уникальный идентификатор записи о заработной плате.
        /// </summary>
        [Key]
        public Guid Salary_Id { get; set; } 

        /// <summary>
        /// Получает или задает размер оклада сотрудника.
        /// </summary>
        public decimal Wage { get; set; }

        /// <summary>
        /// Получает или задает размер премии сотрудника.
        /// </summary>
        public decimal Bonus { get; set; }

        /// <summary>
        /// Получает или задает размер надбавки сотрудника.
        /// </summary>
        public decimal Allowance { get; set; }

        /// <summary>
        /// Получает или задает размер аванса сотрудника.
        /// </summary>
        public decimal Prepayment { get; set; }

        /// <summary>
        /// Получает или задает размер штрафа сотрудника.
        /// </summary>
        public decimal Penalty { get; set; }

        /// <summary>
        /// Получает или задает сотрудника, которому начислена данная заработная плата.
        /// </summary>
        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        /// <summary>
        /// Переопределение метода ToString для удобного представления заработной платы в виде текста.
        /// Возвращает описание мероприятия и сотрудника.
        /// </summary>
        /// <returns>Строковое представление зарплаты — заработная плата и сотрудник.</returns>
        public override string ToString()
        {
            return $"Заработная плата: {FinSalar} - {Employee}";
        }

        /// <summary>
        /// Финальная зпрплата вычисляемое свойство.
        /// Формируется как сумма Wage, Bonus, Allowance и Prepayment, вычитая Penalty
        /// Атрибут <see cref="NotMapped"/> указывает на то, что это свойство не сохраняется в базе данных.
        /// </summary>
        [NotMapped]
        public decimal FinSalar => Wage + Bonus + Allowance + Prepayment - Penalty;
    }
}
