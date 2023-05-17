using System;
using System.Collections.Generic;

namespace MassEffect
{
    partial class StarSystem
    {
        public static void SetupStarSystemGraph()
        {
            Random rnd = new Random();

            foreach (var G in Systems)
            {
                List<StarSystem> Available = new List<StarSystem>(Systems);
                Available.Remove(G);

                foreach (var S in G._adjescent)
                {
                    Available.Remove(S);
                }

                for (int i = 0; i < rnd.Next(1, Available.Count); i++)
                {
                    StarSystem RandomSystem = Available[rnd.Next(0, Available.Count)];
                    G._adjescent.Add(RandomSystem);
                    RandomSystem._adjescent.Add(G);
                }
            }
        }

        public void ShowNeightbors()
        {
            Console.WriteLine("*** _adjescent to {0} ***\n", Name);

            foreach (var G in _adjescent)
            {
                Console.WriteLine("* Star System: {0}\nCurrent mission: {1}", G.Name, G.RootNode.Element.ToString());
            }
        }
    }
}