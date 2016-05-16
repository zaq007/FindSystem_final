using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FindSystem.Models
{
    public class Task
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Answer { get; set; }

        public string Comments { get; set; }

        public int? Position { get; set; }

        public string Img { get; set; }

        public Path Path { get; set; }

    }
}