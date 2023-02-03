using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Core.Domain.ResponseModel
{
    public record StudentResponseModel
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
    }
}
