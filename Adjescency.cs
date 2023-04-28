using System;
using System.Collections.Generic;

namespace MassEffect
{
    partial class StarSystem
    {
        private bool ContainsGalaxy(StarSystem N)
        {
            for (int i = 0; i < Systems.Count; i++)
            {
                if (N == Systems[i])
                {
                    return true;
                }
            }
            return false;
        }

        public static void SetupStarSystemGraph()
        {
            Random rnd = new Random();

            foreach (var G in Systems)
            {
                List<StarSystem> Available = new List<StarSystem>(Systems);
                Available.Remove(G);

                foreach (var S in G.Adjescent)
                {
                    Available.Remove(S);
                }

                for (int i = 0; i < Available.Count; i++)
                {
                    StarSystem RandomSystem;
                    RandomSystem = Systems[rnd.Next(0, Systems.Count)];
                    G.Adjescent.Add(RandomSystem);
                    RandomSystem.Adjescent.Add(G);
                }
            }
        }

        public void ShowNeightbors()
        {
            Console.WriteLine("*** Adjescent to {0} ***\n", Name);

            foreach (var G in Adjescent)
            {
                Console.WriteLine("* Star System: {0}\nCurrent mission: {1}", G.Name, G.RootNode.Element.ToString());
            }
        }
    }
}