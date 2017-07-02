using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Deposit
{
    public static class DepositInterestCalculator
    {        
        /// <summary>
        /// محاسبه سود سپرده های ارزی
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="days"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static float CalculateProfit(float rate, int days, float amount)
        {
            var profit = (amount*rate*days)/36500;
            return profit;
        }
    }
}
