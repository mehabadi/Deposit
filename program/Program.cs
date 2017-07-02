using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Deposit;
using Loan;
using Microsoft.Office.Interop.Excel;

namespace program
{
    class Program
    {
        static void Main()
        {
            var P = 1000000; //مبلغ اصل تسهیلات
            var r = 0.015f; // نرخ سود سالانه به درصد - باید بین 0 و 1 باشد
            var N = 12; // مدت زمان بازپرداخت تسهیلات به ماه

            Console.WriteLine(DepositInterestCalculator.CalculateProfit(100000, 31, 1.75f));
            Console.WriteLine(DepositInterestCalculator.CalculateProfit(200000, 62, 3.75f));
            Console.WriteLine(DepositInterestCalculator.CalculateProfit(350000, 93, 0.25f));
            Console.WriteLine(DepositInterestCalculator.CalculateProfit(400000, 30, 4.75f));
            Console.WriteLine(DepositInterestCalculator.CalculateProfit(500000, 365, 0.8f));
            Console.ReadKey();

            var _loan = LoadCalculator.CalculateMonthlyInstalments(P, r, N);            
            
            var _instalment = LoadCalculator.CalculateInstalments(P, r, N);
            

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                Console.WriteLine("EXCEL could not be started. Check that your office installation and project references are correct.");
                return;
            }
            xlApp.Visible = true;

            Workbook wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = (Worksheet)wb.Worksheets[1];

            if (ws == null)
            {
                Console.WriteLine("Worksheet could not be created. Check that your office installation and project references are correct.");
            }

            if (ws != null)
            {
                ws.Cells[1, 1] = "مبلغ تسهیلات";
                ws.Cells[1, 2] = "نرخ سود سالانه (درصد)";
                ws.Cells[1, 3] = "مدت زمان بازپرداخت (ماه)";
                ws.Cells[1, 4] = "مبلغ قسط ماهانه";
                ws.Cells[1, 5] = "کل سود";
                              
                ws.Cells[2, 1] = P;
                ws.Cells[2, 2] = r;
                ws.Cells[2, 3] = N;
                ws.Cells[2, 4] = _loan.MonthlyInstalment.ToString();
                ws.Cells[2, 5] = _loan.TotalRate.ToString();

                ws.Cells[5, 1] = "شماره قسط";
                ws.Cells[5, 2] = "مانده تسهیلات";
                ws.Cells[5, 3] = "قسط بابت سود";
                ws.Cells[5, 4] = "قسط بابت اصل";
                ws.Cells[5, 5] = "قسط ماهانه";
                var i = 5;
                foreach (var y in _instalment)
                {
                    i++;
                    ws.Cells[i, 1] = y.Month;
                    ws.Cells[i, 2] = y.RemnantAmount;
                    ws.Cells[i, 3] = y.InstallmenProfit;
                    ws.Cells[i, 4] = y.InstallmenPrincipalAmount;
                    ws.Cells[i, 5] = y.MonthlyInstalment;                    
                }

                //Console.WriteLine(DepositInterestCalculator.CalculateProfit(100000, 31, 1.75f));
                //Console.WriteLine(DepositInterestCalculator.CalculateProfit(200000, 62, 3.75f));
                //Console.WriteLine(DepositInterestCalculator.CalculateProfit(350000, 93, 0.25f));
                //Console.WriteLine(DepositInterestCalculator.CalculateProfit(400000, 30, 4.75f));
                //Console.WriteLine(DepositInterestCalculator.CalculateProfit(500000, 365, 0.8f));
            }
        }
    }
}
