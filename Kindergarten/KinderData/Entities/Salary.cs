using System;
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
        public Employee? Employees { get; set; }
        public override string ToString()
        {
            return $"{Wage - Penalty} - {Employees}";
        }
    }
}
