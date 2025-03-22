using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderData.Entities
{
    public class Agreements
    {
        [Key]
        public Guid Agreements_Id { get; set; }
        public string Vacation { get; set; }
        public string SickLeave { get; set; }
        public string Dismissal { get; set; }
        public string EmploymentContract { get; set; }

        [ForeignKey("Employee_Id")]
        public virtual Employee Employee { get; set; }

    }
}
