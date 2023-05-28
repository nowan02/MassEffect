using System;
using System.IO;
using System.Collections.Generic;

namespace MassEffect
{
    public partial class StarSystem : IComparable
    {
        public static List<StarSystem> Systems;
        string _name;

        public string Name
        {
            get { return _name; }
        }

        // This is a set, because for some reason the Adjescency Creator likes to assign the same galaxy as a neighbor twice sometimes.
        HashSet<StarSystem> _adjescent;

        public HashSet<StarSystem> Adjescent
        {
            get { return _adjescent; }
        }

        MissionNode _rootNode;

        public MissionNode RootNode
        {
            get { return _rootNode; }
        }

        public StarSystem(string Name)
        {
            _name = Name;
            _adjescent = new HashSet<StarSystem>();
        }

        public void RemoveCompletedMission(Dictionary<Mission, string> Route)
        {
            Route.Add(_rootNode.Element, this._name);
            _rootNode = _rootNode.NextElement;
        }

        public static void Populate(string FilePath)
        {
            string[] Data = File.ReadAllLines(FilePath);
            string[] MissionData;
            int Row = 0;
            int GalaxyN = 0;

            while (Row < Data.Length)
            {
                switch (Data[Row])
                {
                    case "BEGIN":
                        Systems.Add(new StarSystem(Data[Row + 1]));
                        Row = Row + 2;
                        break;
                    case "END":
                        Row++;
                        GalaxyN++;
                        break;
                    case "":
                        Row++;
                        break;
                    default:
                        MissionData = Data[Row].Split(";");
                        try
                        {
                            if (int.Parse(MissionData[1]) > 10 || int.Parse(MissionData[1]) < 0)
                                throw new InvalidRiskException(MissionData[0], int.Parse(MissionData[1]), Row + 1);

                            if (int.Parse(MissionData[2]) < 0)
                                throw new InvalidRewardException(MissionData[0], int.Parse(MissionData[2]), Row + 1);

                            Mission NewMission = new Mission(MissionData[0], int.Parse(MissionData[1]), int.Parse(MissionData[2]), MissionData[3]);

                            if (Systems[GalaxyN]._rootNode == null)
                            {
                                Systems[GalaxyN]._rootNode = new MissionNode(NewMission);
                            }
                            else
                            {
                                Systems[GalaxyN]._rootNode.Add(new MissionNode(NewMission));
                            }
                        }
                        catch (InvalidCastException)
                        {
                            throw new FileErrorException(FilePath, Row);
                        }
                        Row++;
                        break;
                }
            }
        }

        public int CompareTo(object OtherSystem)
        {
            return (OtherSystem as StarSystem).RootNode.Element.Ratio.CompareTo(_rootNode.Element.Ratio);
        }
    }
}
