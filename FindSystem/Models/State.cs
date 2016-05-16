using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FindSystem.Models
{
    public class State
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? Position { get; set; }

        public bool IsFrozen { get; set; }

        public virtual UserProfile Team { get; set; }

    }
}