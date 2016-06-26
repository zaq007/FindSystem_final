using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;

namespace Data.DTO
{
    public class AnswerResult
    {
        public bool IsRight { get; set; }
        public bool End { get; set; }
        public ITask Task { get; set; }
    }
}
