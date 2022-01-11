namespace School.DataAccess.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentGrade")]
    public partial class StudentGrade
    {
        [Key]
        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }

        public int StudentID { get; set; }

        public decimal? Grade { get; set; }

        public virtual Course Course { get; set; }

        public virtual Person Person { get; set; }
    }
}
