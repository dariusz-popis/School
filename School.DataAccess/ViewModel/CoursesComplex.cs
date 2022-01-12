namespace School.DataAccess.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Model;

    public partial class CoursesComplex
    {
        public IEnumerable<StudentGrade> StudentGrades { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Person> People { get; set; }
    }
}
