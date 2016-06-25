using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class State
    {
        [Key]
        public int UserId { get; set; }

        public int? PathId { get; set; }

        public int Position { get; set; }

        public bool IsFrozen { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
