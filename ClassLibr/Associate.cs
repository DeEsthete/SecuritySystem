using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibr
{
    public class Associate
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }

        public Associate(string name, string surname, string lastname, string position)
        {
            Name = name;
            Surname = surname;
            Lastname = lastname;
            Position = position;
        }

        public Associate()
        {

        }
    }
}
