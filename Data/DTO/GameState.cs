using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;

namespace Data.DTO
{
    public class GameState
    {
        public int UserId { get; set; }
        public ITask Task { get; set; }
        public IEnumerable<IScoreboard> Scoreboard { get; set; }
        public bool IsStarted { get; set; }
        public bool IsFinished { get; set; }
    }
}
