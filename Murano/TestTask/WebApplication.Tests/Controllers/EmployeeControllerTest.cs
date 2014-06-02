using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using Dl.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WebApplication.Controllers;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.Tests.Controllers
{

    [TestClass]
    public class EmployeeControllerTest
    {
        [TestMethod]
        public void GetEmployeesByStatusAndPageNum_Test()
        {
            EmployeeController controller = new EmployeeController();
            const string incomingData = "{\"PageNum\":1,\"CountPerPage\":10,\"IsActive\":true,\"ViewAll\":true}";
            var result = controller.GetEmployeesByStatusAndPageNum(incomingData);
            var deserializedResult = JSonHelpers.DeserializeJSon<EmployeeListModel>(result);
            Assert.IsNotNull(deserializedResult);
            Assert.AreEqual(deserializedResult.CountPerPage, 10);
            Assert.IsTrue(deserializedResult.IsActive);
            Assert.IsTrue(deserializedResult.ViewAll);

            using (var db = new Dl.CompanyContext())
            {
                var employeeCount = db.Employees.Count();
                Assert.AreEqual(employeeCount, deserializedResult.AllCount);
            }
        }

        [TestMethod]
        public void GetEmployeePositions_Test()
        {
            using (var db = new Dl.CompanyContext())
            {
                var positions = db.Positions.OrderBy(p => p.Key).ToList();
                var jPositions = JsonConvert.SerializeObject(positions);

                EmployeeController controller = new EmployeeController();
                var actionResult = controller.GetEmployeePositions();
                Assert.IsNotNull(actionResult);
                Assert.AreEqual(jPositions, actionResult);
                var deserializedActionResult = JSonHelpers.DeserializeJSon<List<Position>>(actionResult);
                Assert.IsNotNull(deserializedActionResult);
                Assert.AreEqual(deserializedActionResult.Count, positions.Count);
            }
        }

        [TestMethod]
        public void UpdateEmployee_Test()
        {
            EmployeeController controller = new EmployeeController();
            const string successIncomingData =
                "{\"Id\":\"ef9fe91a-1306-4392-b636-8881b9a479e1\",\"Name\":\"Adelya Salikhova\",\"PositionId\":4,\"PositionName\":\"Document handling staff\",\"IsActive\":true,\"Salary\":9000,\"Tax\":10,\"After\":8100,\"viewEdit\":true}";

            var succesResult = controller.UpdateEmployee(successIncomingData);
            Assert.AreEqual(JSonHelpers.SerializeJSon(true), succesResult);

            const string errorIncomingData =
               "{\"Id\":\"ef9fe91a-1306-4392-b636-8881b9a479e1\",\"Name\":\"\",\"PositionId\":4,\"PositionName\":\"Document handling staff\",\"IsActive\":true,\"Salary\":9000,\"Tax\":10,\"After\":8100,\"viewEdit\":true}";
            var errorResult = controller.UpdateEmployee(errorIncomingData);
            Assert.AreEqual(JSonHelpers.SerializeJSon(false), errorResult);
        }

        [TestMethod]
        public void CreateEmployee_Test()
        {
            EmployeeController controller = new EmployeeController();
            const string succesIncomingData =
                "{\"Name\":\"Test Data 2\",\"IsActive\":true,\"PositionId\":4,\"Salary\":\"3023423\"}";

            var successResult = controller.CreateEmployee(succesIncomingData);
            Assert.AreEqual(JSonHelpers.SerializeJSon(true), successResult);

            const string errorIncomingData =
                "{\"Name\":\"Test 223\",\"IsActive\":false,\"PositionId\":4,\"Salary\":\"-3023423\"}";
            var errorResult = controller.CreateEmployee(errorIncomingData);
            Assert.AreEqual(JSonHelpers.SerializeJSon(false), errorResult);
        }

        [TestMethod]
        public void GenerateReport_Test()
        {
            // Arrange
            EmployeeController controller = new EmployeeController();

            // Act
            FileStreamResult result = controller.GenerateReport() as FileStreamResult;

            // Assert
            Assert.IsNull(result);
        }
    }
}
