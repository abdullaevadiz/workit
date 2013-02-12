using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations;
using DAL.Entities;

namespace DAL
{
    public class WorkitContext : DbContext
    {
        public WorkitContext():base("WorkitDb")
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobsCity> JobsCities { get; set; }
        public DbSet<JobsType> JobsTypes { get; set; }
        public DbSet<JobsCategory> JobsCategories { get; set; }
        public DbSet<JobsCompany> JobsCompanies { get; set; }

        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<SubscribesEmail> SubscribesEmails { get; set; }

        public DbSet<Logger> Loggers { get; set; }
    }
}
