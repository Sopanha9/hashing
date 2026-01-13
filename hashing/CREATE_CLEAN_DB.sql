-- NEW DATABASE SETUP SCRIPT
-- RUN THIS IN SQL SERVER MANAGEMENT STUDIO (SSMS)
-- This will create a fresh database 'CleanStudentDB' with the correct schema and sample data.

USE master;
GO

-- 1. Create Database (Drop if exists to ensure clean state)
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'CleanStudentDB')
BEGIN
    ALTER DATABASE CleanStudentDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CleanStudentDB;
END
GO

CREATE DATABASE CleanStudentDB;
GO

USE CleanStudentDB;
GO

-- 2. Create Tables

-- Students Table (Now with Course column!)
CREATE TABLE Students (
    StudentID INT PRIMARY KEY,  -- Not Identity, we manage IDs manually as per requirement
    StudentName NVARCHAR(100) NOT NULL,
    Gender NVARCHAR(20),
    DateOfBirth DATETIME,
    Phone NVARCHAR(20),
    Course NVARCHAR(100)
);

-- SystemAdmins Table
CREATE TABLE SystemAdmins (
    AdminID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Teachers Table
CREATE TABLE Teachers (
    TeacherID INT IDENTITY(1,1) PRIMARY KEY,
    TeacherName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100),
    Phone NVARCHAR(20)
);

-- Courses Table
CREATE TABLE Courses (
    CourseID INT IDENTITY(1,1) PRIMARY KEY,
    CourseName NVARCHAR(100) NOT NULL,
    Credits INT DEFAULT 3
);

-- Classes Table
CREATE TABLE Classes (
    ClassID INT IDENTITY(1,1) PRIMARY KEY,
    CourseID INT NOT NULL FOREIGN KEY REFERENCES Courses(CourseID),
    TeacherID INT NOT NULL FOREIGN KEY REFERENCES Teachers(TeacherID),
    Semester NVARCHAR(50),
    RoomNumber NVARCHAR(20)
);

-- Enrollments Table
CREATE TABLE Enrollments (
    EnrollmentID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID INT NOT NULL FOREIGN KEY REFERENCES Students(StudentID) ON DELETE CASCADE,
    ClassID INT NOT NULL FOREIGN KEY REFERENCES Classes(ClassID),
    EnrollmentDate DATETIME DEFAULT GETDATE(),
    Grade DECIMAL(5,2)
);
GO

-- 3. Insert Sample Data (The 6 Students you wanted + Others)

-- Admins
INSERT INTO SystemAdmins (FullName, Username, PasswordHash) VALUES 
('System Admin', 'admin', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f'); -- Pass: admin123

-- Students
INSERT INTO Students (StudentID, StudentName, Gender, DateOfBirth, Phone, Course) VALUES 
(1, 'Sok Pisey', 'Female', '2000-05-15', '012345678', 'Computer Science'),
(2, 'Chan Dara', 'Male', '1999-08-20', '012345679', 'Information Technology'),
(3, 'Meng Sreynit', 'Female', '2001-03-10', '012345680', 'Global Affairs'),
(4, 'Vann Sokha', 'Male', '2000-11-25', '012345681', 'Economy'),
(5, 'Keo Malina', 'Female', '1999-12-05', '012345682', 'Civil Engineering'),
(6, 'Pich Dara', 'Male', '2001-07-18', '012345683', 'Electrical Engineering');

-- Teachers
INSERT INTO Teachers (TeacherName, Email, Phone) VALUES 
('Dr. Sok Vannak', 'vannak@school.edu', '012111222'),
('Prof. Mey Chenda', 'chenda@school.edu', '012111223');

-- Courses
INSERT INTO Courses (CourseName, Credits) VALUES 
('Data Structures', 4),
('Database Systems', 3),
('Web Development', 3);

-- Classes
INSERT INTO Classes (CourseID, TeacherID, Semester, RoomNumber) VALUES 
(1, 1, 'Fall 2024', 'A-101'),
(2, 2, 'Fall 2024', 'B-202');

PRINT '✅ Database CleanStudentDB created successfully with sample data!';
GO
