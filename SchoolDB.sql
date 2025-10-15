USE SchoolDB;
GO

CREATE TABLE tblDepartments (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL
);

INSERT INTO tblDepartments (DepartmentName) VALUES
('Computer Science'),
('Mathematics'),
('Physics'),
('Chemistry');

CREATE TABLE tblInstructors (
    InstructorID INT PRIMARY KEY IDENTITY(1,1),
    InstructorName NVARCHAR(100) NOT NULL,
    DepartmentID INT NOT NULL
);

INSERT INTO tblInstructors (InstructorName, DepartmentID) VALUES
('Dr. John Smith', 1),
('Dr. Jane Doe', 2),
('Dr. Alice Brown', 3),
('Dr. Bob White', 4);

CREATE TABLE tblCourses (
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(100) NOT NULL,
    Credits INT NOT NULL,
    DepartmentID INT NOT NULL,
    InstructorID INT NOT NULL
);

INSERT INTO tblCourses (CourseName, Credits, DepartmentID, InstructorID) VALUES
('Introduction to Computer Science', 3, 1, 1),
('Calculus I', 4, 2, 2),
('Physics I', 4, 3, 3),
('Organic Chemistry', 4, 4, 4);

CREATE TABLE tblStudents (
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    StudentName NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    DepartmentID INT NOT NULL
);

INSERT INTO tblStudents (StudentName, DateOfBirth, DepartmentID) VALUES
('Alice Nguyen', '2000-05-12', 1),
('Bob Tran', '2001-03-18', 2),
('Charlie Le', '1999-09-30', 3),
('Diana Pham', '2002-11-22', 4);

CREATE TABLE tblStudentCourses (
    StudentID INT NOT NULL,
    CourseID INT NOT NULL,
    PRIMARY KEY (StudentID, CourseID),
    FOREIGN KEY (StudentID) REFERENCES tblStudents(StudentID),
    FOREIGN KEY (CourseID) REFERENCES tblCourses(CourseID)
);

INSERT INTO tblStudentCourses (StudentID, CourseID) VALUES
(1, 1),
(1, 2),
(2, 3),
(3, 4),
(4, 1),
(4, 3);

-- tblCourses → tblDepartments
ALTER TABLE tblCourses
ADD CONSTRAINT FK_Courses_Departments
FOREIGN KEY (DepartmentID) REFERENCES tblDepartments(DepartmentID);

-- tblCourses → tblInstructors
ALTER TABLE tblCourses
ADD CONSTRAINT FK_Courses_Instructors
FOREIGN KEY (InstructorID) REFERENCES tblInstructors(InstructorID);

-- tblStudentCourses → tblCourses
ALTER TABLE tblStudentCourses
ADD CONSTRAINT FK_StudentCourses_Courses
FOREIGN KEY (CourseID) REFERENCES tblCourses(CourseID);

-- tblStudentCourses → tblStudents
ALTER TABLE tblStudentCourses
ADD CONSTRAINT FK_StudentCourses_Students
FOREIGN KEY (StudentID) REFERENCES tblStudents(StudentID);

CREATE TABLE tblUsers (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);


INSERT INTO tblUsers (Username, Password) VALUES ('Akhom', '1234');
