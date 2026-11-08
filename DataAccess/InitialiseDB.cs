using FinanceTrackerV2._1.DataTypes;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTrackerV2._1.DataAccess
{
    internal class InitialiseDB
    {


        public InitialiseDB()
        {

            using var DBConnection = new SqliteConnection("Data Source=financetracker_v2_1.db;Foreign Keys=True");
            DBConnection.Open();

            var create = DBConnection.CreateCommand();
            create.CommandText = """                    
                    CREATE TABLE IF NOT EXISTS Goals (
                    GoalID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL, 
                    Amount REAL NOT NULL,
                    Description TEXT
                    );

                    CREATE TABLE IF NOT EXISTS Portfolios (
                    PortfolioID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS BankAccounts (
                    BankAccountID INTEGER PRIMARY KEY AUTOINCREMENT, 
                    PortfolioID INTEGER NOT NULL,
                    AccountName TEXT NOT NULL, 
                    Balance REAL NOT NULL, 
                    FOREIGN KEY (PortfolioID)
                        REFERENCES Portfolios(PortfolioID)
                    );

                    CREATE TABLE IF NOT EXISTS CreditAccounts (
                    CreditAccountID INTEGER PRIMARY KEY AUTOINCREMENT, 
                    PortfolioID INTEGER NOT NULL, 
                    AccountName TEXT NOT NULL, 
                    CreditLimit REAL NOT NULL, 
                    FOREIGN KEY (PortfolioID) 
                        REFERENCES Portfolios(PortfolioID)
                    );

                    CREATE TABLE IF NOT EXISTS BankTransactions (
                    TransactionID INTEGER PRIMARY KEY AUTOINCREMENT,
                    BankAccountID INTEGER NOT NULL,
                    Amount REAL NOT NULL, 
                    Name TEXT NOT NULL, 
                    Category TEXT NOT NULL, 
                    FOREIGN KEY (BankAccountID)
                        REFERENCES BankAccounts (BankAccountID)
                    );

                    CREATE TABLE IF NOT EXISTS CreditTransactions (
                    TransactionID INTEGER PRIMARY KEY AUTOINCREMENT, 
                    CreditAccountID INTEGER NOT NULL,
                    Amount REAL NOT NULL, 
                    Name TEXT NOT NULL,
                    Category TEXT NOT NULL, 
                    FOREIGN KEY (CreditAccountID)
                        REFERENCES CreditAccounts (CreditAccountID)
                    );



                    
                    """;
            create.ExecuteNonQuery();




        }



    }







}
        