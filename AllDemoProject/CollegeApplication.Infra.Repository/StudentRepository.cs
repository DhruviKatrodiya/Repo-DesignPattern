using CollegeApplication.Infra.Contract;
using CollegeApplication.Infra.Domain;
using CollegeApplication.Infra.Domain.Entity;
using CollegeApplication.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Infra.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CollegeApplicationContext _collegeApplicationContext;
        public StudentRepository(CollegeApplicationContext collegeApplicationContext)
        {
            _collegeApplicationContext = collegeApplicationContext;
        }

        public async Task<int> AddStudent(Student student)
        {
            await _collegeApplicationContext.Students.AddAsync(student);
            return await _collegeApplicationContext.SaveChangesAsync();
        }

        

        public async Task<Student?> GetStudent(int studentId)
        {
            var student = await _collegeApplicationContext.Students.FirstOrDefaultAsync(d => d.Id == studentId);
            return student;
        }

        public async Task<PagedList<Student>> GetStudents(string searchTerm = null, int page = 1, int pageSize = 25)
        {
            try
            {
                var students = _collegeApplicationContext.Students.Include(d => d.Department).Where(d => !d.IsDeleted).OrderByDescending(d => d.CreatedOn).AsQueryable();
                //var students1 = _collegeApplicationContext.Students.Include(d => d.Department).Where(d => !d.IsDeleted).OrderByDescending(d => d.CreatedOn).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    students = students.Where(d =>
                               EF.Functions.Like(d.StudentName, $"%{searchTerm}%") ||
                               EF.Functions.Like(d.Email, $"%{searchTerm}%") ||
                               EF.Functions.Like(d.Department.DepartmentName, $"%{searchTerm}%")
                    );
                }
                var count = await students.LongCountAsync();
                var pagedList = students.ToPagedList(page, pageSize, count);

                return pagedList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateStudent(Student student, int studentId)
        {
            var students = await _collegeApplicationContext.Students.Where(x => x.Id == studentId && !x.IsDeleted).FirstAsync();
            students.StudentName = student.StudentName;
            students.Email = student.Email;
            students.BirthDate = student.BirthDate;
            students.DepartmentId = student.DepartmentId;
            students.CvFile = student.CvFile;
            _collegeApplicationContext.Students.Update(students);
            return await _collegeApplicationContext.SaveChangesAsync();
        }

        


    }
}
