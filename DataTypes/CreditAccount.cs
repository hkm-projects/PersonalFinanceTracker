using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTrackerV2._1.DataTypes
{
    /// <summary>
    /// This is a child class of the Account class which is for the user's credit account
    /// </summary> 
    public class CreditAccount : Account
    {
        public int PortfolioID { get; set; }
        public int CreditAccountID { get; set; }
        public double CreditLimit { get; set; }

        /// <summary>
        /// This overrides the retrieve balance method as defined in the Account class  
        /// </summary>
        /// <returns> This returns the calculated balance </returns> 
        public override double RetrieveBalance()
        {
            double creditDebt = 0;
            foreach (Transaction t in Transactions)
            {
                creditDebt += t.Amount;
            }

            double balance = CreditLimit - creditDebt;

            return balance;
        }

        public CreditAccount (int portfolioID, double creditLimit)
        {
            PortfolioID = portfolioID;
            CreditLimit = creditLimit;
        }
    }// CreditAccount C1 = new Credit
}
