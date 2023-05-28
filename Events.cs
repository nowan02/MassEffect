using System;
using System.IO;
using System.Collections.Generic;

namespace MassEffect
{
    public static class Events
    {
        // A dictionary containing the event message as a key, and the penalty in days as the value.
        public static List<string> EventDescriptions = new List<string>();
        public static List<int> EventPenalties = new List<int>();

        public static void ReadEvents(string FilePath)
        {
            string[] Data = File.ReadAllLines(FilePath);

            for (int i = 0; i < Data.Length; i++)
            {
                string[] split = Data[i].Split(";");
                EventDescriptions.Add(split[0]);
                try
                {
                    EventPenalties.Add(int.Parse(split[1]));

                }
                catch (InvalidCastException)
                {
                    throw new FileErrorException(FilePath, i);
                }
            }
        }
    }
}