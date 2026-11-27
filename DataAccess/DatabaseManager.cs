using FinanceTrackerV2._1.DataTypes;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FinanceTrackerV2._1.DataAccess 
{
    internal class DatabaseManager
    {
        private readonly string connectionstring = "Data Source=financetracker_v2_1.db;Foreign Keys=True";

        public void Add(Goal goal)
        {
            using var DBConnection = new SqliteConnection(connectionstring);
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
            using var DBConnection = new SqliteConnection(connectionstring);
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
            using var DBConnection = new SqliteConnection(connectionstring);
            DBConnection.Open();

            var insert = DBConnection.CreateCommand();
            insert.CommandText = """
                INSERT INTO Accounts(PortfolioID,Type,AccountName)
                values($pID,$type,$accname);

                """;

            insert.Parameters.AddWithValue("$pID", bankacc.PortfolioID);
            insert.Parameters.AddWithValue("$type", "BANK");
            insert.Parameters.AddWithValue("$accname", bankacc.Name);
            
            insert.ExecuteNonQuery();

            int accountid = RetrieveAccountID(bankacc.Name);

            var insert1 = DBConnection.CreateCommand();
            insert1.CommandText = """
                INSERT INTO BankAccounts(AccountID, Balance, InterestRate)
                values($accid,$balance,$rate)
                """;
            insert1.Parameters.AddWithValue("$accid", accountid);
            insert1.Parameters.AddWithValue("$balance", bankacc.BankBalance);
            insert1.Parameters.AddWithValue("$rate", bankacc.InterestRate);
            insert1.ExecuteNonQuery();
        }

        public void Add(CreditAccount credacc)
        {

            using var DBConnection = new SqliteConnection(connectionstring);
            DBConnection.Open();

            var insert = DBConnection.CreateCommand();
            insert.CommandText = """
                INSERT INTO Accounts(PortfolioID,Type,AccountName)
                values($pID,$type,$accname);

                """;

            insert.Parameters.AddWithValue("$pID", credacc.PortfolioID);
            insert.Parameters.AddWithValue("$type", "CREDIT");
            insert.Parameters.AddWithValue("$accname", credacc.Name);
            
            insert.ExecuteNonQuery();

            int accountid = RetrieveAccountID(credacc.Name);

            var insert1 = DBConnection.CreateCommand();
            insert1.CommandText = """
                INSERT INTO CreditAccounts (AccountID, CreditLimit)
                VALUES($accid, $limit);

                """;

            insert1.Parameters.AddWithValue("$accid", accountid);
            insert1.Parameters.AddWithValue("$limit", credacc.CreditLimit);

            insert1.ExecuteNonQuery();


        }

        public void Add(Transaction t)
        {
            using var DBConnection = new SqliteConnection(connectionstring);
            DBConnection.Open();

            var insert = DBConnection.CreateCommand();
            insert.CommandText = """
                INSERT INTO Transactions(AccountID,Amount,Name,Category)
                values($aID,$amt,$name,$cat);

                """;

            insert.Parameters.AddWithValue("$aID", t.AccountID);
            insert.Parameters.AddWithValue("$amt", t.Amount);
            insert.Parameters.AddWithValue("$name", t.Name);
            insert.Parameters.AddWithValue("$cat", t.Category);

            insert.ExecuteNonQuery();

        }



        public List<Goal> RetrieveGoals()
        {
            using var DBConnection = new SqliteConnection(connectionstring);
            DBConnection.Open();

            var retrieve = DBConnection.CreateCommand();
            retrieve.CommandText = """
                SELECT GoalID, Amount, Name, Description
                FROM Goals;


                """;

            using var reader = retrieve.ExecuteReader();
            List<Goal> result = new List<Goal>();
            while (reader.Read())
            {
                Goal g = new Goal(
                    reader.GetInt32(0),
                    reader.GetDouble(1),
                    reader.GetString(2),
                    reader.GetString(3)
                );

                result.Add(g);

            }

            return result;
        }

        public int RetrievePortfolioID(string name)
        {
            using var DBConnection = new SqliteConnection(connectionstring);
            DBConnection.Open();

            var idquery = DBConnection.CreateCommand();
            idquery.CommandText = """
                SELECT PortfolioID
                FROM Portfolios
                WHERE Name = $name;
                """;
            idquery.Parameters.AddWithValue("$name",name);
            int p_id = Convert.ToInt32(idquery.ExecuteScalar());

            return p_id;
        }

        public int RetrieveAccountID(string acc_name)
        {
            using var DBConnection = new SqliteConnection(connectionstring);
            DBConnection.Open();
            var idquery = DBConnection.CreateCommand();
            idquery.CommandText = """
                SELECT AccountID
                FROM Accounts
                WHERE AccountName = $accname;
                """;
            idquery.Parameters.AddWithValue("$accname", acc_name);            
            int acc_id = Convert.ToInt32(idquery.ExecuteScalar());
            return acc_id;

        }

       public List<Portfolio> RetrievePortfolios()
        {
            using var DBConnection = new SqliteConnection(connectionstring);
            DBConnection.Open();

            var retrieve = DBConnection.CreateCommand();
            retrieve.CommandText = """
                SELECT 
                IFNULL(p.PortfolioID,-1) as ValidPortfolio,
                IFNULL(a.AccountID,-1) as ValidAccount,
                IFNULL(t.TransactionID,-1) as ValidTransaction,
                p.PortfolioID,p.Name as PortfolioName,
                a.AccountID, a.Type, a.AccountName, 
                b.InterestRate,b.Balance,
                c.CreditLimit,
                t.TransactionID,t.Amount,t.name as TransactionName,t.Category
                FROM Portfolios p 
                LEFT JOIN Accounts a on p.PortfolioID = a.PortfolioID 
                LEFT JOIN BankAccounts b on a.AccountID = b.AccountID
                LEFT JOIN CreditAccounts c on a.AccountID = c.AccountID
                LEFT JOIN Transactions t on a.AccountID = t.AccountID
                ORDER BY p.PortfolioID, 
                a.AccountID,
                t.TRANSACTIONID;
                """;


            List<Portfolio> result = new List<Portfolio>();
            using var reader = retrieve.ExecuteReader();



            while (reader.Read())
            {
                Portfolio currentPortfolio = null;
                Account currentAccount = null;
                bool found = false;

                int validP = Convert.ToInt32(reader["ValidPortfolio"]);

                if(validP == -1)
                {
                    continue;
                }



                int portfolioID = Convert.ToInt32( reader["PortfolioID"]);

                foreach (Portfolio p in result)
                {
                    if (p.PortfolioID == portfolioID)
                    {
                        found = true;
                        currentPortfolio = p;
                    }

                }

                if (!found)
                {
                    string p_name = (string)reader["PortfolioName"];
                    currentPortfolio = new Portfolio(portfolioID, p_name);
                    result.Add(currentPortfolio);
                }

                found = false;

                int validA = Convert.ToInt32(reader["ValidAccount"]);

                if(validA == -1)
                {
                    continue;
                }

                int accountID = Convert.ToInt32(reader["AccountID"]);

                foreach (Account a in currentPortfolio.Accounts)
                {
                    if (a.AccountID == accountID)
                    {
                        found = true;
                        currentAccount = a;

                    }
                }

                if (!found)
                {
                    string type = (string)reader["Type"];
                    string acc_name = (string)reader["AccountName"];
                    if (type == "BANK")
                    {

                        double interestRate = (double)reader["InterestRate"];
                        double balance = (double)reader["Balance"];
                        BankAccount b = new BankAccount(accountID, currentPortfolio.PortfolioID, acc_name, interestRate, balance);
                        currentPortfolio.Accounts.Add(b);
                        currentAccount = b;
                    }

                    else
                    {
                        double limit = (double)reader["CreditLimit"];
                        CreditAccount c = new CreditAccount(accountID, currentPortfolio.PortfolioID, acc_name, limit);
                        currentPortfolio.Accounts.Add(c);
                        currentAccount = c;
                    }


                }

                found = false;

                int validT = Convert.ToInt32(reader["ValidTransaction"]);
                if (validT == -1)
                {
                    continue;
                }

                int transactionID = Convert.ToInt32(reader["TransactionID"]);
                Transaction currentTransaction = null;

                foreach (Transaction t in currentAccount.Transactions)
                {

                    if (transactionID == t.TransactionID)
                    {
                        currentTransaction = t;
                        found = true;
                    }

                }

                if (!found)
                {
                    double amount = (double)reader["Amount"];
                    string t_name = (string)reader["TransactionName"];
                    string category = (string)reader["Category"];
                    Transaction t = new Transaction(currentAccount.AccountID, amount, t_name, transactionID, category);
                    currentAccount.Transactions.Add(t);
                    currentTransaction = t;
                }

            }
            return result;

        } 


        public List<Account> RetrieveAccounts(int portfolioID)
        {

            using var DBConnection = new SqliteConnection(connectionstring);
            DBConnection.Open();

            var retrieve = DBConnection.CreateCommand();

            retrieve.CommandText = """
                SELECT AccountID, PortfolioID, Type, AccountName
                FROM Accounts 
                Where PortfolioID = $pid;

                """; 

            retrieve.Parameters.AddWithValue("$pid", portfolioID);

            using var reader = retrieve.ExecuteReader(); 
            List<Account> result = new List<Account>();

            while (reader.Read())
            {
                Account a = new Account();
                a.AccountID = reader.GetInt32(0);
                a.PortfolioID = reader.GetInt32(1);
                a.Name = reader.GetString(2);

                result.Add(a);
            }

            return result;

        }

    }
}

    

