using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTrackerV2._1.DataTypes
{
    public class Goal
    {
        public int GoalID;

        public double Amount;

        public string Name;

        public string Description;

        public Goal (int goalID, double amount, string name, string description)
        {
            GoalID = goalID;
            Amount = amount;
            Name = name;
            Description = description;
        }

        public Goal()
        {

        }

        public string UpdateGoal()
        {
            string UpdatedGoal = "";
            return UpdatedGoal;

        }


        public float GoalProgress()
        {
            float Progress = 0;
            return Progress;
        }
    }
}
