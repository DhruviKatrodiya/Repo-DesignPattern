using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CollegeApplication.Core.Builder;
using CollegeApplication.Core.Contract;
using CollegeApplication.Core.Domain.Exceptions;
using CollegeApplication.Core.Domain.RequestModel;
using CollegeApplication.Core.Domain.ResponseModel;
using CollegeApplication.Core.Service.Helper;
using CollegeApplication.Infra.Contract;
using CollegeApplication.Infra.Domain;
using CollegeApplication.Shared;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Core.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly FileUploadHelper _fileUploadHelper;
        private readonly CollegeApplicationContext _context;
        public StudentService(IStudentRepository studentRepository, IMapper mapper, FileUploadHelper fileUploadHelper, CollegeApplicationContext context)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _fileUploadHelper = fileUploadHelper;
            _context = context;
        }
        public async Task AddStudentAsync(StudentRequestModel student)
        {
            try
            {
                var fileKey = await _fileUploadHelper.UploadFile(student.CvFile);
                var students = StudentBuilder.Build(student, fileKey);
                var count = await _studentRepository.AddStudent(students);
                if(count == 0)
                {
                    throw new BadRequestException("Student is not Created Successfully");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            try
            {
                var student = await _studentRepository.GetStudent(studentId);

                if (student == null)
                {
                    throw new NotFoundException($"Candidate with {studentId} is not found.");
                }

                student.Delete();
                var count = await _studentRepository.UpdateStudent(student,studentId);
                if(count == 0)
                {
                    throw new BadRequestException("Student is not updated Successfully.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }





        public async Task<string> DownloadStudentCv(int studentId)
        {
            try
            {
                var student = await _studentRepository.GetStudent(studentId);

                if (student == null)
                {
                    throw new NotFoundException($"Candidate with {studentId} is not found.");
                }
                var file = _fileUploadHelper.DownloadCVFile(studentId);

                return file.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }




        public async Task<PagedList<StudentResponseModel>> GetStudentAsync(string searchTerm = null, int page = 1, int pageSize = 25)
        {
            try
            {
                var students = await _studentRepository.GetStudents(searchTerm, page, pageSize);
                var result = _mapper.Map<PagedList<StudentResponseModel>>(students);
                return result;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task UpdateStudent(StudentRequestModel student, int studentId)
        {
            try
            {
                var fileKey = await _fileUploadHelper.UploadFile(student.CvFile);
                var students = StudentBuilder.Build(student, fileKey);
                var count = await _studentRepository.UpdateStudent(students,studentId);
                if (count == 0)
                {
                    throw new BadRequestException("Student is not Updated Successfully");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
