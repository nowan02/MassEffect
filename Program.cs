using System;
using System.Collections.Generic;

namespace MassEffect
{
    class Program
    {
        static void Main(string[] args)
        {
            StarSystem.Systems = new List<StarSystem>();

            try
            {
                StarSystem.Populate("test");
            }
            catch (FileErrorException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0},\nPath: {1}, Row: {2}", ex.Message, ex.FileName, ex.FileRow + 1);
                Console.WriteLine("Skipping mission...");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (InvalidRiskException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0}, Risk: {1}, Mission: {2}", ex.Message, ex.Risk, ex.MissionName);
                Console.WriteLine("Skipping mission...");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (InvalidRewardException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0}, Reward: {1}, Mission: {2}", ex.Message, ex.Reward, ex.MissionName);
                Console.WriteLine("Skipping mission...");
                Console.ForegroundColor = ConsoleColor.White;
            }

            string Output = "All systems:\n";

            foreach (var G in StarSystem.Systems)
            {
                Output += ($"{G.Name}\n" + G.RootNode.PrintAllMissions() + "\n");
            }

            System.Console.WriteLine(Output);

            StarSystem.SetupStarSystemGraph();

            foreach (var G in StarSystem.Systems)
            {
                G.ShowNeightbors();
            }

            SpaceShip The = new SpaceShip(100, 100);

            The.Move(The.Current, new List<StarSystem>(The.Current.Adjescent)[0]);
        }
    }
}