using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderData.Entities
{
    public class EmployeeData
    {
        [Key]
        public Guid Employee_Id { get; set; }
        public int Pasport { get; set; }
        public int SNILS { get; set; }
        public int INN { get; set; }
        public string EmploymentRecord { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

        [ForeignKey("Employee_Id")]
        public virtual Employee Employee { get; set; }
    }
}
