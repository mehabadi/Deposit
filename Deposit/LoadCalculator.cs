using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan
{
    public static class LoadCalculator
    {
        /// <summary>
        /// محاسبه اقساط ماهانه و کل سود
        /// 
        /// P: مبلغ اصل تسهیلات
        /// r: نرخ سود سالانه به درصد
        /// N: مدت زمان بازپرداخت تسهیلات به ماه
        /// x: تعداد کل اقساط قابل پرداخت در طول سال
        /// </summary>
        /// <returns>قسط ماهانه و کل سود</returns>
        public static Instalments CalculateMonthlyInstalments(float P, float R, int N, int x = 12)
        {
            var result = new Instalments();
            if (R < 0 || R > 1)
            {
                throw new System.ArgumentException("نرخ سود سالانه باید بین 0 و یک باشد.", "Error");
            }
            result.MonthlyInstalment = (float) (((P * R / x) * Math.Pow(1 + (R / x), N)) / (Math.Pow(1+(R/x), N) - 1));
            result.TotalRate = N * result.MonthlyInstalment - P;
            return result;            
        }
        /// <summary>
        /// محاسبه اقساط تسهیلات
        /// 
        /// P: مبلغ اصل تسهیلات
        /// r: نرخ سود سالانه به درصد
        /// N: مدت زمان بازپرداخت تسهیلات به ماه
        /// x: تعداد کل اقساط قابل پرداخت در طول سال
        /// </summary>
        /// <returns></returns>
        public static List<Instalments> CalculateInstalments(float P, float R, int N, int x = 12)
        {
            var results = new List<Instalments>();
            for (var i = 1; i <= N; i++)
            {
                var result = new Instalments();
                result = CalculateMonthlyInstalments(P, R, N);

                result.Month = i;
                result.TotalMonths = N;
                
                result.RemnantAmount = (float) ((P*Math.Pow(1 + R/x, (i-1))) - ((result.MonthlyInstalment/(R/x)) * (Math.Pow(1 + R/x, (i-1)) - 1)));
                result.InstallmenProfit = (float) Math.Pow(1 + R/x, (i - 1)) * ((P * (R/x)) - result.MonthlyInstalment) +result.MonthlyInstalment;
                result.InstallmenPrincipalAmount = (float)Math.Pow(1 + R / x, (i - 1)) * (result.MonthlyInstalment - (P * (R/x)));
                results.Add(result);
            }            
            return results;
        }
    }
}
