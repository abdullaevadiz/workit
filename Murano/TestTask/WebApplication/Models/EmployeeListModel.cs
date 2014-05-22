using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dl.Classes;

namespace WebApplication.Models
{
    public class EmployeeListModel
    {
        public List<Employee> Employees { get; set; }
        public int PageNum { get; set; }
        public int CountPerPage { get; set; }
        public bool IsActive { get; set; }
        public bool ViewAll { get; set; }
        public long AllCount { get; set; }

        public EmployeeListModel()
        {
            Employees = new List<Employee>();
            PageNum = 0;
            IsActive = true;
            ViewAll = true;
            AllCount = 0;
            CountPerPage = 0;
        }
    }
}