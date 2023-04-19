using System;

namespace MassEffect
{
    public class Mission : IComparable
    {
        int Risk;
        int Reward;
        string Name;
        string Description;

        public Mission(int Risk, int Reward, string Name, string Description)
        {
            this.Risk = Risk;
            this.Reward = Reward;
            this.Name = Name;
            this.Description = Description;
        }

        public Mission(string MissionData)
        {
            string[] M = MissionData.Split(';');
            try
            {
                Risk = int.Parse(M[0]);
                if (Risk < 0 || Risk > 10)
                {
                    Console.WriteLine("Risk factor wasn't between 0 - 10, generating random risk...");
                    Risk = new Random().Next(0, 11);
                }
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("Risk factor wasn't a number, generating random risk...");
                Risk = new Random().Next(0, 11);
            }
        }

        public override string ToString()
        {
            return $"{Name}\n{Description}\nRisk: {Risk}\nReward: {Reward}\n";
        }

        public int CompareTo(object OtherMission)
        {
            return Reward.CompareTo((OtherMission as Mission).Reward);
        }
    }
}