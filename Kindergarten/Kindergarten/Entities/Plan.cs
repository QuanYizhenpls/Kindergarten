using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderData.Entities
{
    public class Plan
    {
        [Key]
        public Guid Plan_Id { get; set; }
        public DateOnly DateOfTheEvent { get; set; }
        public string Development { get; set; }
        [ForeignKey("Group_Id")]
        public virtual Group Group { get; set; }
        [ForeignKey("Employee_Id")]
        public virtual Employee Employee { get; set; }
    }
}
