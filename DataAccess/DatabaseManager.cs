using FinanceTrackerV2._1.DataTypes;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTrackerV2._1.DataAccess
{
    internal class DatabaseManager
    {
        private readonly string con = "Data Source=financetracker.db;Foreign Keys=True";

        public void Add(Goal goal)
        {
            using var DBConnection = new SqliteConnection("Data Source=financetracker_v2_1.db;Foreign Keys=True");
            DBConnection.Open();

            var insert = DBConnection.CreateCommand();
            insert.CommandText = """
                    INSERT INTO Goals(Name, Amount, Description)
                    VALUES ($Gname,$Gamt,$Gdesc);

                    """;

            insert.Parameters.AddWithValue("$Gname", goal.Name);
            insert.Parameters.AddWithValue("$Gamt", goal.Amount);
            insert.Parameters.AddWithValue("$Gdesc", goal.Description);
            insert.ExecuteNonQuery();


        }

        public void Add(Portfolio portfolio)
        {
            using var DBConnection = new SqliteConnection("Data Source=financetracker_v2_1.db;Foreign Keys=True");
            DBConnection.Open();

            var insert = DBConnection.CreateCommand();
            insert.CommandText = """
                INSERT INTO Portfolios (Name)
                Values ($name);
                """;
            insert.Parameters.AddWithValue("$name", portfolio.Name);
            insert.ExecuteNonQuery();

        }

        public void Add(BankAccount bankacc)
        {
            using var DBConnection = new SqliteConnection("Data Source=financetracker_v2_1.db;Foreign Keys=True");
            DBConnection.Open();

            var insert = DBConnection.CreateCommand();
            insert.CommandText = """
                INSERT INTO BankAccounts(PortfolioID,AccountName,Balance)
                values($pID,$accname,$balance);

                """;

            insert.Parameters.AddWithValue("$pID", bankacc.PortfolioID);
            insert.Parameters.AddWithValue("$accname", bankacc.Name);
            insert.Parameters.AddWithValue("$balance", bankacc.RetrieveBalance());
            insert.ExecuteNonQuery();

        }

        public void Add(CreditAccount credacc)
        {

            using var DBConnection = new SqliteConnection("Data Source=financetracker_v2_1.db;Foreign Keys=True");
            DBConnection.Open();

            var insert = DBConnection.CreateCommand();
            insert.CommandText = """
                INSERT INTO CreditAccounts(PortfolioID,AccountName,CreditLimit)
                values($pID,$accname,$limit);

                """;

            insert.Parameters.AddWithValue("$pID", credacc.PortfolioID);
            insert.Parameters.AddWithValue("$accname", credacc.Name);
            insert.Parameters.AddWithValue("$limit", credacc.CreditLimit);
            insert.ExecuteNonQuery();
        }

        public void Add (Transaction transaction, BankAccount bankacc)
            
        {
            using var DBConnection = new SqliteConnection("Data Source=financetracker_v2_1.db;Foreign Keys=True");
            DBConnection.Open();

            var insert = DBConnection.CreateCommand();
            insert.CommandText = """
                INSERT INTO BankTransactions(BankAccountID, Amount, Name, Category)
                VALUES ($bankID, $amount, $name, $category);

                """;
            insert.Parameters.AddWithValue("$bankID", bankacc.BankAccountID);
            insert.Parameters.AddWithValue("$amount", transaction.Amount);
            insert.Parameters.AddWithValue("$name", transaction.Name);
            insert.Parameters.AddWithValue("$category", transaction.Category);
            insert.ExecuteNonQuery();
            

        }

        public void Add (Transaction transaction, CreditAccount credacc)
        {
            using var DBConnection = new SqliteConnection("Data Source=financetracker_v2_1.db;Foreign Keys=True");
            DBConnection.Open();

            var insert = DBConnection.CreateCommand();
            insert.CommandText = """
                INSERT INTO CreditTransactions(CreditAccountID, Amount, Name, Category)
                VALUES ($credID, $amount, $name, $category);

                """;

            insert.Parameters.AddWithValue("$credID", credacc.CreditAccountID);
            insert.Parameters.AddWithValue("$amount", transaction.Amount);
            insert.Parameters.AddWithValue("$name", transaction.Name);
            insert.Parameters.AddWithValue("$category", transaction.Category);
            insert.ExecuteNonQuery();
        }



    }
}

    

