using System;
using System.Linq;
using System.Collections.Generic;

namespace MassEffect
{
    public class SpaceShip
    {
        record CrewMember
        {
            public string Name;
            public string Title;
            public int HP;
        }

        List<StarSystem> _known;

        public List<StarSystem> Known
        {
            get { return _known; }
            set { _known = value; }
        }

        StarSystem Current;

        List<CrewMember> Crew;
        int HP;
        int FuelLevel;

        public SpaceShip(int HP, int FuelLevel)
        {
            this.HP = HP;
            this.FuelLevel = FuelLevel;
            this._known = new List<StarSystem>();
            this.Current = StarSystem.Systems[0];
            _known.Add(Current);
        }

        public int ShortestPath(StarSystem Start, StarSystem End, List<StarSystem> Visited)
        {
            List<int> PathLengths = new List<int>();

            if (Start.Adjescent.Contains(End)) return 1;

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

        public void AddCrewMember(string Name, string Title)
        {
            CrewMember C = new CrewMember();
            C.Name = Name;
            C.Title = Title;
            C.HP = 100;

            Crew.Add(C);
        }

        public void Heal(string Name, int Heal)
        {
            Crew.Find(C =>
            {
                if (C.Name == Name) return true;
                return false;
            }).HP += Heal;
        }

        private void _removeCrewMember(string Name)
        {
            Crew.Remove(Crew.Find(C =>
            {
                if (C.Name == Name) return true;
                return false;
            }));
        }

        private void _takeDamage(string Name, int DMG)
        {
            Crew.Find(C =>
            {
                if (C.Name == Name) return true;
                return false;
            }).HP -= DMG;
        }
    }
}