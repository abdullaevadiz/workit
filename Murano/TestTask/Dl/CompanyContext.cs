using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dl.Classes;

namespace Dl
{
    public class CompanyContext : DbContext
    {
        public CompanyContext() : base("DefaultConnection")
        {
            var _ = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //configure model with fluent API 
            modelBuilder.Entity<Employee>()
              .HasRequired(a => a.Position)
              .WithMany()
              .HasForeignKey(u => u.PositionId);
        }

        public class CompanyContextInitializer : DropCreateDatabaseIfModelChanges<CompanyContext>
        {
            protected override void Seed(CompanyContext context)
            {
                var positions = new List<Position>
            {
                new Position {Id = 0, Key = "Receptionist"},
                new Position {Id = 1, Key = "Marketing Staff"},
                new Position {Id = 2, Key = "Sales Staff"},
                new Position {Id = 3, Key = "Accounting department staff"},
                new Position {Id = 4, Key = "Product management staff"},
                new Position {Id = 5, Key = "Document handling staff"},
                new Position {Id = 6, Key = "Supporting staff"},
                new Position {Id = 7, Key = "One master mind"}
            };
                foreach (var item in positions)
                {
                    context.Positions.Add(item);
                }

                var rnd = new Random();
                var employees = new List<Employee>
            {
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Adelya Salikhova", PositionId = rnd.Next(0, 7), Salary = 9000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Dasha Brunilina", PositionId = rnd.Next(0, 7), Salary = 10000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Dilyara Sharafutdinova", PositionId = rnd.Next(0, 7), Salary = 23000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Djakhangir Dadadjanov", PositionId = rnd.Next(0, 7), Salary = 26000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Eugenia Nechaeva", PositionId = rnd.Next(0, 7), Salary = 6000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = false, Name = "Fruma Rosenfeld", PositionId = rnd.Next(0, 7), Salary = 16000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Ilya Mirolyubov", PositionId = rnd.Next(0, 7), Salary = 28000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Ilana Kozak", PositionId = rnd.Next(0, 7), Salary = 11000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Helen Kim", PositionId = rnd.Next(0, 7), Salary = 6000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Inessa Khanova", PositionId = rnd.Next(0, 7), Salary = 11000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Irina Levitanus", PositionId = rnd.Next(0, 7), Salary = 26000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Julia Fomina", PositionId = rnd.Next(0, 7), Salary = 8000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Maria Chechina", PositionId = rnd.Next(0, 7), Salary = 16000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Mike Kozak", PositionId = rnd.Next(0, 7), Salary = 14000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Oksana Vaulina", PositionId = rnd.Next(0, 7), Salary = 13000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Olga Mirolyubova", PositionId = rnd.Next(0, 7), Salary = 6000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Tamara Andreeva", PositionId = rnd.Next(0, 7), Salary = 9000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Valerya Aksenova", PositionId = rnd.Next(0, 7), Salary = 9000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Vladimir Rozin", PositionId = rnd.Next(0, 7), Salary = 9000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = false, Name = "Yevgeniy Ilyin", PositionId = rnd.Next(0, 7), Salary = 9000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = false, Name = "Yuliya Ogay", PositionId = rnd.Next(0, 7), Salary = 9000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = false, Name = "Zukhra Kasimova", PositionId = rnd.Next(0, 7), Salary = 9000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Slava Kurdov", PositionId = rnd.Next(0, 7), Salary = 9000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = false, Name = "Jenny Andreyeva", PositionId = rnd.Next(0, 7), Salary = 9000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = false, Name = "Julia Ilisirov", PositionId = rnd.Next(0, 7), Salary = 9000 },
                new Employee{ Id = Guid.NewGuid(), IsActive = true, Name = "Kazakevich Yefim", PositionId = rnd.Next(0, 7), Salary = 9000 }
            };
                foreach (var item in employees)
                {
                    context.Employees.Add(item);
                }

                base.Seed(context);
            }


        }
    }
}
