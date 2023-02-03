using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Infra.Domain.Entity
{
    public class Student : Audit
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public string CvFile { get; set; }
        public virtual Department Department { get; set; }

        public Student()
        {

        }

        public Student(string studentName,DateTime birthDate,string email,int departmentId, string cvFile)
        {
            StudentName = studentName;
            BirthDate = birthDate;
            Email = email;
            DepartmentId = departmentId;
            CvFile = cvFile;
            CreatedOn = DateTime.UtcNow;
            IsDeleted = false;
        }

        public Student Delete()
        {
            IsDeleted = true;
            UpdatedOn = DateTime.Now;
            return this;
        }
    }
}
