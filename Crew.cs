using System;
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

        List<CrewMember> Crew;
        int HP;
        int FuelLevel;

        public SpaceShip(int HP, int FuelLevel)
        {
            this.HP = HP;
            this.FuelLevel = FuelLevel;
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