using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public partial class Path_Task
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int PathId { get; set; }
        public int Position { get; set; }

        public virtual Task Task { get; set; }
        public virtual Path Path { get; set; }
    }
}
