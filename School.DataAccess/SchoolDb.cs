using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace School.DataAccess
{
    using Model;
    public partial class SchoolDb : DbContext
    {
        public SchoolDb()
            : base("name=SchoolDb")
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public virtual DbSet<OnlineCourse> OnlineCourses { get; set; }
        public virtual DbSet<OnsiteCourse> OnsiteCourses { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<StudentGrade> StudentGrades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasOptional(e => e.OnlineCourse)
                .WithRequired(e => e.Course);

            modelBuilder.Entity<Course>()
                .HasOptional(e => e.OnsiteCourse)
                .WithRequired(e => e.Course);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.StudentGrades)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.People)
                .WithMany(e => e.Courses)
                .Map(m => m.ToTable("CourseInstructor").MapLeftKey("CourseID").MapRightKey("PersonID"));

            modelBuilder.Entity<Department>()
                .Property(e => e.Budget)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OfficeAssignment>()
                .Property(e => e.Timestamp)
                .IsFixedLength();

            modelBuilder.Entity<Person>()
                .HasOptional(e => e.OfficeAssignment)
                .WithRequired(e => e.Person);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.StudentGrades)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.StudentID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StudentGrade>()
                .Property(e => e.Grade)
                .HasPrecision(3, 2);
        }
    }
}
