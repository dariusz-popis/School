﻿namespace School.DataAccess
{
    using Dapper;
    using School.DataAccess.Helpers;
    using School.DataAccess.Schema;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;

    public class StudentGradeRepository : BaseRepository
    {
        private static readonly string[] tvtGradesColumnNames = new string[] { "Id", "Grade" };

        public StudentGradeRepository(string connectionString = null) : base(connectionString) { }

        public int SetGrades(IEnumerable<IdValue<decimal>> grades, string loginName)
        {
            var dt = grades.Map(tvtGradesColumnNames, "WebRepository.Grade");
            int result = 0;
            try
            {
                result = Connection.Execute(WebRepository.StudentGrade_SetGrade, new { Grades = dt, LoginName = loginName}, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
