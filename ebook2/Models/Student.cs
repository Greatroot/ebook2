using System;
using System.Collections.Generic;

namespace ebook2.Models
{
    public partial class Student
    {
        public Student()
        {
            Issurance = new HashSet<Issurance>();
        }

        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Grade { get; set; }

        public ICollection<Issurance> Issurance { get; set; }
    }
}
