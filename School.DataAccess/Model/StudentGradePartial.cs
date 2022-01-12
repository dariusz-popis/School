namespace School.DataAccess.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class StudentGrade
    {
        [NotMapped]
        [Display(Name = "Set")]
        public bool HasToUpdate { get; set; }
    }
}
