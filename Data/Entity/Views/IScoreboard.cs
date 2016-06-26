using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public interface IScoreboard
    {
        int Id { get; set; }
        int Position { get; set; }
        string UserName { get; set; }
        bool IsFinished { get; set; }
    }
}
