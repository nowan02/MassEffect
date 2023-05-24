using System;
using System.IO;
using System.Collections.Generic;

namespace MassEffect
{
    public static class Events
    {
        // A dictionary containing the event message as a key, and the amount of days that it will cost as the value.
        public static Dictionary<string, int> SeriesOfUnfortunateEvents = new Dictionary<string, int>();

        public static void ReadEvents(string FilePath)
        {
            string[] Data = File.ReadAllLines(FilePath);

            for (int i = 0; i < Data.Length; i++)
            {
                string[] split = Data[i].Split(";");
                try
                {
                    SeriesOfUnfortunateEvents.Add(split[0], int.Parse(split[1]));
                }
                catch (InvalidCastException)
                {
                    throw new FileErrorException(FilePath, i);
                }
            }
        }
    }
}