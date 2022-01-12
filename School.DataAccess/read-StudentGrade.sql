use School
go

select sg.EnrollmentID, sg.CourseID, sg.StudentID, sg.Grade
     , concat(std.FirstName,' ',std.LastName) FullName
     , crs.Title
  from StudentGrade sg
  join Person std
    on std.PersonID = sg.StudentID
  join Course crs
    on crs.CourseID = sg.CourseID
 order by crs.Title, FullName