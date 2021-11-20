using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloMVCWorld.Models
{
    public class WebUser
    {
        public string FirstName { get; set; }
        public bool isMale { get; set; }
        public DayOfWeek day { get; set; }
        public string LastName { get; set; }
    }

    public enum DayOfWeek
    {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6
    }

}
