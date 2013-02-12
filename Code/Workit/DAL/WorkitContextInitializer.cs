using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL
{
    public class WorkitContextInitializer : DropCreateDatabaseAlways<WorkitContext>
    {
        protected override void Seed(WorkitContext context)
        {
            #region JobsTypes

            var jobsTypes = new List<JobsType>
                            {
                                new JobsType { Id = 1, Name = "Полная занятость" },
                                new JobsType { Id = 2, Name = "Свободный график" },
                                new JobsType { Id = 3, Name = "Контракт" },
                                new JobsType { Id = 4, Name = "Фриланс" },
                                new JobsType { Id = 5, Name = "Одноразовая работа"}
                            };
            jobsTypes.ForEach(t => context.JobsTypes.Add(t));
            context.SaveChanges();
            #endregion

            #region JobsCategories
            var jobsCategories = new List<JobsCategory>
                                     {
                                        new JobsCategory {Id = 1, Name = "Разное"},
                                        new JobsCategory { Id = 2, Name = "Дизайн и юзабилити" },
                                        new JobsCategory { Id = 3, Name = "Фронтенд" },
                                        new JobsCategory { Id = 4, Name = "Бэкенд" },
                                        new JobsCategory { Id = 5, Name = "Приложения" },
                                        new JobsCategory { Id = 6, Name = "Управление и менеджмент" },
                                        new JobsCategory { Id = 7, Name = "Контент" },
                                        new JobsCategory { Id = 8, Name = "Администрирование" },
                                        new JobsCategory { Id = 9, Name = "Тестирование" }
                                     };

            jobsCategories.ForEach(c => context.JobsCategories.Add(c));
            context.SaveChanges();
            #endregion

            #region JobsCities
            var jobsCities = new List<JobsCity>
                                 {
                                    new JobsCity {Id = 1, State = "Ташкент", Districts = "Бектемирский"},
                                    new JobsCity { Id = 2, State = "Ташкент", Districts = "Мирабадский" },
                                    new JobsCity { Id = 3, State = "Ташкент", Districts = "Мирзо-Улугбекский" },
                                    new JobsCity { Id = 4, State = "Ташкент", Districts = "Сергелийский" },
                                    new JobsCity { Id = 5, State = "Ташкент", Districts = "Олмазарский" },
                                    new JobsCity { Id = 6, State = "Ташкент", Districts = "Учтепинский" },
                                    new JobsCity { Id = 7, State = "Ташкент", Districts = "Хамзинский" },
                                    new JobsCity { Id = 8, State = "Ташкент", Districts = "Чиланзарский" },
                                    new JobsCity { Id = 9, State = "Ташкент", Districts = "Шайхонтохурский" },
                                    new JobsCity { Id = 10, State = "Ташкент", Districts = "Юнусабадский" },
                                    new JobsCity { Id = 11, State = "Ташкент", Districts = "Яккасарайский" }
                                 };
         
            jobsCities.ForEach(city => context.JobsCities.Add(city));
            context.SaveChanges();
            #endregion

            #region Users
            var user = new User
                           {
                               Id = Guid.NewGuid(),
                               Email = "adizabdullaev@yandex.ru",
                               IsActive = true,
                               Password = "qwe123QWE!@##",
                               RegistrationDate = DateTime.Now
                           };
            user.Token = user.Id.ToString().Replace("-", string.Empty).Take(10).ToString().ToLower();
            context.Users.Add(user);
            #endregion

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
