using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTrackerV2._1.DataTypes
{
    public class Portfolio
    {
        public string Name;
        public int PortfolioID;


        List<Account> Accounts;

        public Portfolio(int portfolioid, string name)
        {
            PortfolioID = portfolioid;
            Name = name;
            Accounts = new List<Account>();
        }

        public Portfolio()
        {
            Accounts = new List<Account>();
        }
        /// <summary>
        /// This is a method which allows the user to add an account to their portfolio 
        /// </summary>
        /// <param name="a">This is an account to be added to the portfolio</param>
        public void AddAccount(Account a)
        {
            Accounts.Add(a);
        }





        public double TotalBalance()
        {

            double balance = 0;

            foreach (Account a in Accounts)
            {
                balance += a.RetrieveBalance();

            }

            return balance;
        }
    }
}
