using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibr
{
    public class Attendance
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public DateTime Date { get; set; }
        public bool Presence { get; set; }

        public Attendance(string name, string surename, string lastname, string position, DateTime date, bool presence)
        {
            Name = name;
            Surname = surename;
            Lastname = lastname;
            Position = position;
            Date = date;
            Presence = presence;
        }

        public Attendance()
        {

        }
    }
}
