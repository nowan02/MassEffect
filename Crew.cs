using System;
using System.Linq;
using System.Collections.Generic;

namespace MassEffect
{
    public class SpaceShip
    {
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

        int HP;
        int FuelLevel;
        int RemainingDays;

        public SpaceShip(int HP, int FuelLevel)
        {
            this.HP = HP;
            this._known = new List<StarSystem>();
            this._current = StarSystem.Systems[0];
            this.RemainingDays = 10;
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

        public void Move(StarSystem Start, StarSystem End)
        {
            RemainingDays -= ShortestPath(Start, End, new List<StarSystem>());

            if (!_known.Contains(End))
            {
                Console.WriteLine("NEW SYSTEM DISCOVERED: {0}", End.Name);
                _known.Add(End);
                _known.Sort();
            }

            Console.WriteLine("Traveled to {0} from {1}, {2} days remain", End.Name, Start.Name, RemainingDays);
            _current = End;
        }
    }
}