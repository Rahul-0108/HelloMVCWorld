using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelloMVCWorld.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Display(Name = "Name")]
        public string StudentName { get; set; }
        public int Age { get; set; }
    }
}
