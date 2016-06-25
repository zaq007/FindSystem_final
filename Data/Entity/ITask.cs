using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public interface ITask
    {
        int Id { get; set; }
        string Description { get; set; }
        string Answer { get; set; }
        string Url { get; set; }
    }
}
