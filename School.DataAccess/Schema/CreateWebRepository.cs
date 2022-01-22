using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DataAccess.Schema
{
    using System.Configuration;
    using System.Data.SqlClient;
    using Dapper;

    public class CreateWebRepository
    {
        private readonly SqlConnection _con;

        public CreateWebRepository(string connectionString = null) => _con = new SqlConnection(connectionString ?? ConfigurationManager.ConnectionStrings[nameof(SchoolDb)].ConnectionString);
        public void Create()
        {
            CreateSchema();
            CreateGradeType();
            CreateStudentGradeSetGrade();
        }
        public void CreateSchema()
        {
            try
            {
                _con.Execute("create schema WebRepository");
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
