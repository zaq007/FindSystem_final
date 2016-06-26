using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;

namespace DAL.Entity
{
    [Table("Scoreboard")]
    public class Scoreboard : IScoreboard
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public string UserName { get; set; }
        public bool IsFinished { get; set; }
    }
}
