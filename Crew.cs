using System;
using System.Linq;
using System.Collections.Generic;

namespace MassEffect
{
    public class SpaceShip
    {
        //For statistics
        Dictionary<Mission, string> _completed;
        int _value;

        List<StarSystem> _known;

        public List<StarSystem> Known
        {
            get { return _known; }
        }

        StarSystem _current;

        public StarSystem Current
        {
            get { return _current; }
        }

        int _remainingDays;

        public int RemainingDays
        {
            get { return _remainingDays; }
            set { _remainingDays = value; }
        }

        public SpaceShip(int RemainingDays)
        {
            this._completed = new Dictionary<Mission, string>();
            this._known = new List<StarSystem>();
            this._current = StarSystem.Systems[0];
            this._remainingDays = RemainingDays;
            _known.Add(_current);
        }

        public int ShortestPath(StarSystem Start, StarSystem End, List<StarSystem> Visited)
        {
            if (Start.Adjescent.Contains(End)) return 1;

            List<int> PathLengths = new List<int>();

            foreach (var V in Start.Adjescent)
            {
                Visited.Add(Start);
                if (_known.Contains(V) && !Visited.Contains(V))
                {
                    PathLengths.Add(ShortestPath(V, End, Visited) + 1);
                }
                else
                {
                    return StarSystem.Systems.Count + 1;
                }
            }

            return PathLengths.Min();
        }

        // Traverse the system
        public void Travel(StarSystem Start, StarSystem End)
        {
            _remainingDays -= ShortestPath(Start, End, new List<StarSystem>());

            if (!_known.Contains(End))
            {
                Console.WriteLine("NEW SYSTEM DISCOVERED: {0}", End.Name);
                _known.Add(End);
                _known.Sort();
            }

            Console.WriteLine("Traveled to {0} from {1}", End.Name, Start.Name);
            _current = End;
        }

        public void ListKnown(List<StarSystem> KnownAndNeighboring)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("### Currently known galaxies, ordered by increasing reward/risk ratio: ###");
            for (int i = 0; i < KnownAndNeighboring.Count; i++)
            {
                if (_known.Contains(KnownAndNeighboring[i]))
                {
                    if (KnownAndNeighboring[i].Equals(_current))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    Console.WriteLine("{0} {1}", i + 1, KnownAndNeighboring[i].Name.ToUpper());
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("{0} UNDISCOVERED NEIGHBORING GALAXY", i + 1);
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("* Distance:\n{0}", KnownAndNeighboring[i].Equals(_current) ? "  You are here" : "   " + ShortestPath(_current, KnownAndNeighboring[i], new List<StarSystem>()));
                Console.WriteLine("* Current Mission:");
                Console.WriteLine("  " + KnownAndNeighboring[i].RootNode.Element.ToString());
            }
        }

        public void DoMission()
        {
            Random rnd = new Random();
            _current.RemoveCompletedMission(_completed);
            _remainingDays--;

            if (rnd.Next(0, 11) > _current.RootNode.Element.RiskFactor) return;

            int e = rnd.Next(0, Events.EventDescriptions.Count);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Something terrible happened!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Events.EventDescriptions[e]);
            Console.WriteLine("You lost {0} days from your time limit", Events.EventPenalties[e]);
            _remainingDays -= Events.EventPenalties[e];
        }
    }
}