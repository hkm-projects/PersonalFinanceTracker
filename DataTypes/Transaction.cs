using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTrackerV2._1.DataTypes
{
    public class Transaction
    {
        public int AccountID;

        public double Amount;

        public string Name;
        
        public int TransactionID;
        
        public string Category;


        public Transaction()
        {

        }

        public Transaction(int accountID, double amount, string name, int transactionID, string category)
        {
            AccountID = accountID;
            Amount = amount;
            Name = name;
            TransactionID = transactionID;
            Category = category;
        }
    }
}
