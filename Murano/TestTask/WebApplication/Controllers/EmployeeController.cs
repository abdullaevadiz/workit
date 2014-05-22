using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}