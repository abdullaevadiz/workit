using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Dl.Classes;
using Newtonsoft.Json;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpPost]
        public string GetEmployeesByStatusAndPageNum(string jData)
        {
            EmployeeListModel incomingData;
            try
            {
                incomingData = JSonHelpers.DeserializeJSon<EmployeeListModel>(jData);
            }
            catch (Exception)
            {
                //Todo: exception not logged
                return JsonConvert.SerializeObject(new EmployeeListModel());
            }

            using (var db = new Dl.CompanyContext())
            {
                var employees = db.Employees.AsQueryable();
                if (!incomingData.ViewAll)
                    employees = employees.Where(e => e.IsActive == incomingData.IsActive);

                var result = new EmployeeListModel
                {
                    AllCount = 0,
                    Employees = new List<Employee>(),
                    PageNum = incomingData.PageNum < 1 ? 1 : incomingData.PageNum,
                    IsActive = incomingData.IsActive,
                    ViewAll = incomingData.ViewAll,
                    CountPerPage = incomingData.CountPerPage < 0 ? 0 : incomingData.CountPerPage
                };

                result.AllCount = employees.Count();
                if (result.AllCount == 0)
                    return JsonConvert.SerializeObject(result);

                var temp =
                    from emps in
                        (employees.OrderBy(e => e.Name)
                            .Skip((result.PageNum - 1)*result.CountPerPage)
                            .Take(result.CountPerPage))
                    join pos in db.Positions
                        on emps.PositionId equals pos.Id
                    select new {em = emps, pos};

                foreach (var item in temp)
                {
                    result.Employees.Add(new Employee
                    {
                        Id = item.em.Id,
                        IsActive = item.em.IsActive,
                        Name = item.em.Name,
                        PositionId = item.em.PositionId,
                        Salary = item.em.Salary,
                        Position = new Position {Key = item.pos.Key}
                    });
                }
                return JsonConvert.SerializeObject(result);
            }
        }

        [HttpGet]
        public string GetEmployeePositions()
        {
            using (var db = new Dl.CompanyContext())
            {
                var result = db.Positions.OrderBy(p => p.Key).ToList();
                return JsonConvert.SerializeObject(result);
            }
        }

        [HttpPost]
        public string UpdateEmployee(string jEmployee)
        {
            var errorResult = JsonConvert.SerializeObject(false);
            Employee incomingEmployee;
            try
            {
                incomingEmployee = JSonHelpers.DeserializeJSon<Employee>(jEmployee);
            }
            catch (Exception)
            {
                //Todo: exception not logged
                return errorResult;
            }

            if (string.IsNullOrEmpty(incomingEmployee.Name))
                return errorResult;

            if (incomingEmployee.Salary < 0)
                return errorResult;

            using (var db = new Dl.CompanyContext())
            {
                var employee = db.Employees.FirstOrDefault(e => e.Id == incomingEmployee.Id);
                if (employee == null)
                    return errorResult;

                var position = db.Positions.FirstOrDefault(p => p.Id == incomingEmployee.PositionId);
                if (position == null)
                    return errorResult;

                employee.Name = incomingEmployee.Name;
                employee.PositionId = incomingEmployee.PositionId;
                employee.Salary = incomingEmployee.Salary;
                employee.IsActive = incomingEmployee.IsActive;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    //Todo: exception not logged
                    return errorResult;
                }
            }
            return JsonConvert.SerializeObject(true);
        }

        [HttpPost]
        public string CreateEmployee(string jEmployee)
        {
            var errorResult = JsonConvert.SerializeObject(false);
            Employee incomingEmployee;
            try
            {
                incomingEmployee = JSonHelpers.DeserializeJSon<Employee>(jEmployee);
            }
            catch (Exception)
            {
                //Todo: exception not logged
                return errorResult;
            }

            if (string.IsNullOrEmpty(incomingEmployee.Name))
                return errorResult;

            if (incomingEmployee.Salary < 0)
                return errorResult;

            using (var db = new Dl.CompanyContext())
            {
                var position = db.Positions.FirstOrDefault(p => p.Id == incomingEmployee.PositionId);
                if (position == null)
                    return errorResult;

                var employee = new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = incomingEmployee.Name,
                    PositionId = incomingEmployee.PositionId,
                    IsActive = incomingEmployee.IsActive,
                    Salary = incomingEmployee.Salary
                };

                try
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    //Todo: exception not logged
                    return errorResult;
                }
            }
            return JsonConvert.SerializeObject(true);
        }

        public ActionResult GenerateReport()
        {
            const string reportNameColumn = "Имя";
            const string reportSalaryColumn = "Заработная плата";
            const string reportTaxColumn = "Сумма налога на з/п";
            const string reportTaxAfterSalaryColumn = "Заработная плата с вычетом налога";

            var reportsLines = new List<string> { string.Format("{0, -30} | {1, -20} | {2, -20} | {3, -20}", reportNameColumn, reportSalaryColumn, reportTaxColumn, reportTaxAfterSalaryColumn) };
            double salaries = 0;
            double tax = 0;
            double salariesAfterTax = 0;
            using (var db = new Dl.CompanyContext())
            {
                var activeEmployees = db.Employees.Where(e => e.IsActive).OrderBy(e => e.Name);
                foreach (var item in activeEmployees)
                {
                    salaries += item.Salary;
                    salariesAfterTax += item.GetSalaryAfterTax();
                    tax += item.GetTax();
                    reportsLines.Add(string.Format("{0, -30} | {1, -20} | {2, -20} | {3, -20}", item.Name, item.Salary, item.GetTax(), item.GetSalaryAfterTax()));
                }
            }
            reportsLines.Add(string.Format("{0, -30} | {1, -20} | {2, -20} | {3, -20}", "Итого", salaries, tax, salariesAfterTax));
            return File(Encoding.UTF8.GetBytes(string.Join("\r\n", reportsLines)), "text/plain", "report.txt");
        }
    }
}