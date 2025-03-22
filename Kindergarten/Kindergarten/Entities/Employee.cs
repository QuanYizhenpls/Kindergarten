using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderData.Entities
{
    public class Employee
    {
        [Key]
        public Guid Employee_Id { get; set; }
        public string FIO { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string Post { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

        [ForeignKey("Group_Id")]
        public virtual Group Group { get; set; }
    }
}
