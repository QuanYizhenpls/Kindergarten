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
    public class Salary
    {
        [Key]
        public Guid Salary_Id { get; set; }
        public decimal Wage { get; set; }
        public decimal Bonus { get; set; }
        public decimal Allowance { get; set; }
        public decimal Prepayment { get; set; }
        public decimal Penalty { get; set; }
        [ForeignKey("Employee_Id")]
        public virtual Employee Employee { get; set; }
    }
}
