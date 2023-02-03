using CollegeApplication.Core.Contract;
using CollegeApplication.Core.Domain.RequestModel;
using CollegeApplication.Infra.Domain;
using CollegeApplication.Infra.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CollegeApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegeApplicationController : ControllerBase
    {
        private readonly IStudentService _collegeApplicationContext;
        private readonly CollegeApplicationContext _context;

        public CollegeApplicationController(IStudentService studentService,CollegeApplicationContext context)
        {
            _collegeApplicationContext = studentService;
            _context = context;
        }

        [HttpGet("getStudents")]
        public async Task<IActionResult> GetStudents(string? searchTerm = null,int page = 1,int pageSize = 25)
        {
            var students = await _collegeApplicationContext.GetStudentAsync(searchTerm, page, pageSize);
            return Ok(students);
        }

        [HttpPost("addStudent")]
        public async Task<IActionResult> AddStudents([FromForm] StudentRequestModel student) 
        { 
            await _collegeApplicationContext.AddStudentAsync(student);
            return Created("students", null);
        }

        [HttpDelete("student/{id}")]
        public async Task<IActionResult> DeleteStudents(int id)
        {
            await _collegeApplicationContext.DeleteStudentAsync(id);
            return NoContent();
        }

        [HttpPut("updateStudent")]
        public async Task<IActionResult> UpdateStudents([FromForm] StudentRequestModel student, int studentId)
        {
            await _collegeApplicationContext.UpdateStudent(student,studentId);
            return Ok(student);
        }

        [HttpGet("DownloadCV")]
        public async Task<IActionResult> DownloadCV(string filename)
        {
            //var file = await _collegeApplicationContext.DownloadStudentCv(studentId);
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"E:\Bigscal\Asp.net\AllDemoProject\CollegeApplication.API\FileDownloaded\", filename);
            var provider = new FileExtensionContentTypeProvider();
            if(!provider.TryGetContentType(filepath, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contentType, Path.GetFileName(filepath));

            ////Response.Headers.Add("Content-Disposition", $"inline; filename={file}");
            ////return File(System.IO.File.ReadAllBytes(file), "application/pdf");
            ////return File(file, "application/pdf");
            //return Ok(file);

            //var memory = DownloadFile(file, @"E:\Bigscal\Asp.net\AllDemoProject\CollegeApplication.API\FileDownloaded\");
            //return File(memory.ToArray(), "application/pdf");


        }


        /*private MemoryStream DownloadFile(string filename, string uploadPath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(),uploadPath, filename);
            var memory = new MemoryStream();
            if (System.IO.File.Exists(path))
            {
                var net = new System.Net.WebClient();
                var data = net.DownloadData(path);
                var content = new System.IO.MemoryStream(data);
                memory = content;
            }
            memory.Position = 0;
            return memory;
        }*/


    }
}
