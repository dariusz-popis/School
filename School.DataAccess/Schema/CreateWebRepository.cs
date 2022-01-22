namespace School.DataAccess.Schema
{
    using Dapper;
    using System.Data.SqlClient;

    public class CreateWebRepository : BaseRepository
    {
        private readonly SqlConnection _con;

        public CreateWebRepository(string connectionString = null) : base(connectionString) { }

        public void Create()
        {
            CreateSchema();
            CreateGradeType();
            CreateStudentGradeSetGrade();
            CreateLog();
        }
        public void CreateSchema()
        {
            try
            {
                _con.Execute("create schema WebRepository");
            }
            catch { }
        }
        public void CreateLog()
        {
            try
            {
                _con.Execute(@"create table WebRepository.Log 
                               (
                                   Id int identity (1, 1) not null,
                                   Date datetime not null,
                                   Thread varchar (255) not null,
                                   Level varchar (50) not null,
                                   Logger varchar (255) not null,
                                   Message varchar (4000) not null,
                                   Exception varchar (2000) null
                               )");
            }
            catch { }
        }
        public void CreateGradeType()
        {
            _con.Execute(@"create type WebRepository.Grade as table
                           (
                              Id int not null,
                              Grade decimal(3,2) null
                           )"
                         );
        }
        public void CreateStudentGradeSetGrade()
        {
            try
            {
                _con.Execute(@"create proc WebRepository.StudentGrade_SetGrade @Grades WebRepository.Grade readonly
                                   as
                               update StudentGrade 
                                  set Grade = g.Grade 
                                 from StudentGrade sg
                                 join @Grades g
                                   on g.Id = sg.EnrollmentID");
            }
            catch { }
        }
    }
}
