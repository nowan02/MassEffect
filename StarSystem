using System;
using System.IO;
using System.Collections.Generic;

namespace MassEffect
{
    public class MissionNode
    {
        Mission Element;
        MissionNode NextElement;

        public MissionNode(Mission Element)
        {
            this.Element = Element;
        }

        public void Add(MissionNode Element)
        {
            if (NextElement != null)
            {
                NextElement.Add(Element);
            }
            else
            {
                NextElement = Element;
            }
        }
    }

    public class StarSystem
    {
        static StarSystem[] Systems;
        string Name;
        StarSystem[] Adjescent;
        MissionNode RootMission;

        public StarSystem(string Name, StarSystem[] Adjescent, MissionNode RootMission)
        {
            this.Name = Name;
            this.Adjescent = Adjescent;
            this.RootMission = RootMission;
        }
    }
}