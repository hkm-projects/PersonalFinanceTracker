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

            var idquery = DBConnection.CreateCommand();
            idquery.CommandText = """
                SELECT AccountID
                FROM Accounts
                WHERE AccountName = $accname;
                """;
            idquery.Parameters.AddWithValue("$accname", bankacc.Name);
            int accountid = Convert.ToInt32(idquery.ExecuteScalar());


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
                INSERT INTO CreditAccounts(PortfolioID,AccountName,CreditLimit)
                values($pID,$accname,$limit);

                """;

            insert.Parameters.AddWithValue("$pID", credacc.PortfolioID);
            insert.Parameters.AddWithValue("$accname", credacc.Name);
            insert.Parameters.AddWithValue("$limit", credacc.CreditLimit);
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
                Goal g = new Goal();
                g.GoalID = reader.GetInt32(0);
                g.Amount = reader.GetDouble(1);
                g.Name = reader.GetString(2);
                g.Description = reader.GetString(3);

                result.Add(g);

            }

            return result;
        }

        public int RetreivePortfolioID(string name)
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



    }
}

    

