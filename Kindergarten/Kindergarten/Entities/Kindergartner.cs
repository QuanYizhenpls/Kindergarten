using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderData.Entities
{
    public class Kindergartner
    {
        [Key]
        public Guid Kindergartner_Id { get; set; }
        public string FIO { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string ParentsContactInfo { get; set; }
    }
}
