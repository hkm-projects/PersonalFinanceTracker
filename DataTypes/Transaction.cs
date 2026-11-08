using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTrackerV2._1.DataTypes
{
    public class Transaction
    {
        public double Amount { get; set; }
        public string Name { get; set; }
        public int TransactionID { get; set; }
        public string Category { get; set; }
    }
}
