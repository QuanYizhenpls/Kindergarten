using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderData.Entities
{
    public class Group
    {
        [Key]
        public Guid Groups_Id { get; set; }
        public Guid GroupsName { get; set; }
        public virtual ICollection<Kindergartner> Kindergartners { get; set; }
        [ForeignKey("Kindergartner_Id")]
        public virtual Kindergartner Kindergartner { get; set; }
    }
}
