using CollegeApplication.Core.Domain.RequestModel;
using CollegeApplication.Infra.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Core.Builder
{
    public class StudentBuilder
    {
        public static Student Build(StudentRequestModel model, string cvKey, string createdByUserId = "")
        {
            return new Student(model.StudentName, model.BirthDate, model.Email, model.DepartmentId, cvKey);
        }
    }
}
