using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTrackerV2._1.DataTypes
{
    public class BankAccount : Account
    {
        public int PortfolioID { get; set; }
        public int BankAccountID { get; set; }
        public float InterestRate { get; set; }
        public float BankBalance { get; set; }

        public BankAccount(int portfolioID, float interestRate, float bankBalance)
        {
            PortfolioID = portfolioID;
            InterestRate = interestRate;
            BankBalance = bankBalance;
        }

        public BankAccount()
        {

        }

        public float CalculateInterest()
        {
            float Interest = 0;
            return Interest;
        }

        /// <summary>
        /// This overrides the retrieve balance method as defined in the Account class  
        /// </summary>
        /// <returns> This returns the calculated balance </returns>

        public override double RetrieveBalance()
        {
            double transactionLoss = 0;
            foreach (Transaction t in Transactions)
            {
                transactionLoss += t.Amount;
            }

            double balance = BankBalance - transactionLoss;

            return balance;
        }
    }
}
