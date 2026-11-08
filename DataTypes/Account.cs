using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTrackerV2._1.DataTypes
{
    /// <summary>
    /// This is the Account class which is used to hold a list of transactions. 
    /// </summary> 
    public class Account
    {
        public string Name;
        public int AccountID;
        public int PortfolioID { get; set; }



        public List<Transaction> Transactions; // A list is used here to allow addition and removal of as many transactions as necessary

        /// <summary>
        /// This is a constructor which initialises the list of transactions
        /// </summary>

        public Account()
        {
            Transactions = new List<Transaction>();
        }

        /// <summary>
        /// This method is used to add a transaction to the list of transactions
        /// </summary>
        /// <param name="newTransaction"> this is a new transaction that must be added to the list</param>
        public void AddTransaction(Transaction newTransaction)
        {
            Transactions.Add(newTransaction);
        }

        /// <summary>
        /// This method is used for the removal of a previous transaction from the list of transactions 
        /// </summary>
        /// <param name="pastTransaction">this is a previous transaction that is to be removed from the list</param>
        public void RemoveTransaction(Transaction pastTransaction)
        {
            foreach (Transaction t in Transactions)
            {
                if (t.TransactionID == pastTransaction.TransactionID)
                {
                    Transactions.Remove(t);
                    break;
                }
            }

        }
        /// <summary>
        /// This is a virtual method that is not expected to be called, the sub classes for Account will implement the actual calculation of balance
        /// </summary>
        /// <returns>It will return the balance for a given account</returns>
        public virtual double RetrieveBalance()
        {

            return 0;
        }


    }
}
