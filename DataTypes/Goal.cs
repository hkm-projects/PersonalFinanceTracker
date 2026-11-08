using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTrackerV2._1.DataTypes
{
    public class Goal
    {
        public int GoalID { get; set; }
        public double Amount { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

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
