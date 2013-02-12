using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Enums
    {
        public enum JobsTypes
        {
            FullTime = 1,
            FreeTime = 2,
            Contract = 3,
            FreeLanсe = 4,
            OneTime = 5
        }

        public enum JobsCategories
        {
            Other = 1,
            DesignAndUsability = 2,
            FrontEnd = 3,
            BackEnd = 4,
            Apps = 5,
            Management = 6,
            Content = 7,
            Administration = 8,
            Testing = 9
        }
    }
}
