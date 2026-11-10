using FinanceTrackerV2._1.DataAccess;
using FinanceTrackerV2._1.DataTypes;
using System.Data.Common;
namespace FinanceTrackerV2._1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BankAccount bankOne = new BankAccount();
            bankOne.Name = "MyBankAccount";
            bankOne.InterestRate = 5;
            bankOne.BankBalance = 2000;

            Transaction t1 = new Transaction();
            t1.Name = "Deliveroo";
            t1.Category = "Food";
            t1.Amount = 50;
            t1.TransactionID = 1;

            Transaction t2 = new Transaction();
            t2.Name = "Nike";
            t2.Category = "Clothes";
            t2.Amount = 100;
            t2.TransactionID = 2;

            CreditAccount credOne = new CreditAccount(7,0.22);
            credOne.Name = "MyCreditCard";
            credOne.CreditLimit = 20000;


            Transaction t3 = new Transaction();
            t3.Name = "Amazon";
            t3.Category = "Shopping";
            t3.Amount = 70;
            t3.TransactionID = 3;

            Transaction t4 = new Transaction();
            t4.Name = "Cinema";
            t4.Category = "Entertainment";
            t4.Amount = 30;
            t4.TransactionID = 4;

            bankOne.AddTransaction(t1);
            bankOne.AddTransaction(t2);

            credOne.AddTransaction(t3);
            credOne.AddTransaction(t4);

            Portfolio myPortfolio = new Portfolio();
            myPortfolio.AddAccount(bankOne);
            myPortfolio.AddAccount(credOne);
            Console.WriteLine("The total balance of your portfolio is " + myPortfolio.TotalBalance());

            InitialiseDB create = new InitialiseDB();
            DatabaseManager test = new DatabaseManager();
            Goal g1 = new Goal(5, 500.26, "goalOne", "");
            test.Add(g1);
            Goal g2 = new Goal(0, 900.35, "goaltwo", "");
            test.Add(g2);
            Goal g3 = new Goal(0, 700.85, "goalthree", "");
            test.Add(g3);

            var result = test.RetrieveGoals();

            Portfolio p1 = new Portfolio(0,"portfolio_one");
            


            test.Add(p1);
            int p_id = test.RetrievePortfolioID(p1.Name);
            
            BankAccount bank1 = new BankAccount(0, p_id, "bankone", 5.33, 2000.75);
            

            test.Add(bank1);


            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}