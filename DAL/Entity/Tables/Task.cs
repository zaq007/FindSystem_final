using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;

namespace DAL.Entity
{
    public partial class Task : ITask
    {
        public Task()
        {
            this.Path_Task = new HashSet<Path_Task>();
            this.Pathes = new HashSet<Path>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Path_Task> Path_Task { get; set; }
        public virtual ICollection<Path> Pathes { get; set; }
    }
}
