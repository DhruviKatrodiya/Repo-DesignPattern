using CollegeApplication.Core.Domain.RequestModel;
using CollegeApplication.Core.Domain.ResponseModel;
using CollegeApplication.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Core.Contract
{
    public interface IStudentService
    {
        Task AddStudentAsync(StudentRequestModel student);

        Task UpdateStudent(StudentRequestModel student,int studentId);

        Task<string> DownloadStudentCv(int studentId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedList<StudentResponseModel>> GetStudentAsync(string searchTerm = null,int page = 1, int pageSize = 25);
        Task DeleteStudentAsync(int studentId);
    }
}
