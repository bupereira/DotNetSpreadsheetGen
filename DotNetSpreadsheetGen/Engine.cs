using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSpreadsheetGen
{
    class SpreadsheetUtils
    {
        public Boolean IsWeekDay(DateTime dtDate)
        {
            return (dtDate.DayOfWeek.ToString() != "Saturday" && 
                    dtDate.DayOfWeek.ToString() != "Sunday");
        }
    }
}
