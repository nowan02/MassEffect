using System;
using System.Collections.Generic;

namespace MassEffect
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            StarSystem.Systems = new List<StarSystem>();

            Console.WriteLine("Loading Galaxies and Missions...\n");
            try
            {
                StarSystem.Populate("test");
            }
            catch (FileErrorException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0},\nPath: {1}, Row: {2}", ex.Message, ex.FileName, ex.FileRow + 1);
                Console.WriteLine("Please fix the errors and start again!");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            catch (InvalidRiskException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0}, Risk: {1}, Mission: {2}", ex.Message, ex.Risk, ex.MissionName);
                Console.WriteLine("Please fix the errors and start again!");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            catch (InvalidRewardException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0}, Reward: {1}, Mission: {2}", ex.Message, ex.Reward, ex.MissionName);
                Console.WriteLine("Please fix the errors and start again!");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.WriteLine("Loading Events...\n");
            try
            {
                Events.ReadEvents("eventtest");
            }
            catch (FileErrorException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0},\nPath: {1}, Row: {2}", ex.Message, ex.FileName, ex.FileRow + 1);
                Console.WriteLine("Please fix the errors and start again!");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.WriteLine("Setting up the graph...\n");
            StarSystem.SetupStarSystemGraph();

            SpaceShip OurShip = new SpaceShip(50);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ALL OK!\n");

            // Game Loop
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("############");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("MASS EFFECT");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("############");

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome!\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("In this game, you are tasked to discover a system of galaxies and complete missions withing them.");
            Console.WriteLine("You will discover them as you go along, some missions will be more valuable than others.\n");
            Console.WriteLine("You will have a limited number of days to complete as many missions as you can, each will take one day to complete.");
            Console.WriteLine("Travelling between galaxies will also cost you a day.");
            Console.WriteLine("The spaceship you are operating will remember all the places you went to,\n and can calculate which mission is best to take according to it's risk and reward.\n");
            Console.WriteLine("You can complete any mission you'd like regardless of what's best according to the calculations, but be careful!");
            Console.WriteLine("Completing missions with high risk might cost you extra days if you're unlucky.\n");
            Console.WriteLine("With that in mind, have a good game!");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nPress any key to continue");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            Console.Clear();

            while (OurShip.RemainingDays > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("{0} Days remain...", OurShip.RemainingDays);
                Console.ForegroundColor = ConsoleColor.White;

                List<StarSystem> G = new List<StarSystem>(OurShip.Known);
                foreach (var neighbor in OurShip.Current.Adjescent)
                {
                    if (!G.Contains(neighbor)) G.Add(neighbor);
                }

                Console.WriteLine("You can control the ship via this command prompt, type HELP for all commands:");
                string[] _userInput = Console.ReadLine().Split(" ");

                switch (_userInput[0].ToUpper())
                {
                    case "HELP":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Available commands:");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("* HELP - List all commands");
                        Console.WriteLine("* DO - Do the mission on the galaxy you are currently on");
                        Console.WriteLine("* LIST - List all known galaxies and missions");
                        Console.WriteLine("* TRAVEL <number> - Travel to the galaxy by giving it's corresponding index from the LIST command");
                        Console.WriteLine("* CLEAR - Clears the console");
                        break;
                    case "DO":
                        OurShip.DoMission();
                        break;
                    case "LIST":
                        OurShip.ListKnown(G);
                        break;
                    case "TRAVEL":
                        int _index = 0;
                        try
                        {
                            _index = int.Parse(_userInput[1]) - 1;
                        }
                        catch (InvalidCastException)
                        {
                            Console.WriteLine("Invalid argument");
                            break;
                        }

                        if (_index >= G.Count)
                        {
                            Console.WriteLine("We don't know about that many galaxies");
                            break;
                        }

                        if (OurShip.Current.Equals(G[_index]))
                        {
                            Console.WriteLine("You are already here");
                            break;
                        }

                        OurShip.Travel(OurShip.Current, G[_index]);
                        break;
                    case "CLEAR":
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
        }
    }
}