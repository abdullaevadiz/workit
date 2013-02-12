using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Core.Loggers
{
    public static class ExeptionLogger
    {
        public static void AddExeption(Exception ex, string methodName, DAL.WorkitContext context)
        {
            context.Loggers.Add(new Logger {Id = Guid.NewGuid(), MethodName = methodName, Message = ex.Message, ExeptionTime =  DateTime.Now });
            context.SaveChanges();
        }
    }
}
