using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loan
{
    public class Instalments
    {
        public float MonthlyInstalment { get; set; } // قسط ماهانه
        public float TotalRate { get; set; } // کل سود

        public int Month { get; set; }// ماه
        public int TotalMonths { get; set; }// کل اقساط
        public float InstallmenProfit { get; set; } // قسط بابت سود
        public float InstallmenPrincipalAmount { get; set; } // قسط بابت اصل
        public float RemnantAmount { get; set; } // مانده تسهیلات
    }
}
