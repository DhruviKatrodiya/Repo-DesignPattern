using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Core.Domain.RequestModel
{
    public record StudentRequestModel
    {
        public string StudentName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public IFormFile CvFile { get; set; }
    }
}
