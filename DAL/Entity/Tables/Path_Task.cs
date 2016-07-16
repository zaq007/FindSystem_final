using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public partial class Path_Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int PathId { get; set; }
        public int Position { get; set; }

        public virtual Task Task { get; set; }
        public virtual Path Path { get; set; }
    }
}
