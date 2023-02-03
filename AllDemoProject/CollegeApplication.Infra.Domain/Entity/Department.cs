using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Infra.Domain.Entity
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }

        protected Department()
        {

        }

        public Department(int id,string departmentName)
        {
            Id = id;
            DepartmentName = departmentName;
        }
    }
}
