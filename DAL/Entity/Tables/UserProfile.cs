using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public partial class UserProfile
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string TeamName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        [ForeignKey("Id")]
        public virtual State State { get; set; }
    }
}
