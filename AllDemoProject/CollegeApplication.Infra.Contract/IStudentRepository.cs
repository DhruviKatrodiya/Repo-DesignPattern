using CollegeApplication.Infra.Domain.Entity;
using CollegeApplication.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Infra.Contract
{
    public interface IStudentRepository
    {
        Task<int> AddStudent(Student student);
        Task<PagedList<Student>> GetStudents(string searchTerm = null,int page = 1,int pageSize = 25);
        Task<Student?> GetStudent(int studentId);
        Task<int> UpdateStudent(Student student,int studentId);
    }
}
