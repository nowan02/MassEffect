using System;
using System.IO;
using System.Collections.Generic;

namespace MassEffect
{
    public partial class StarSystem
    {
        public static List<StarSystem> Systems;
        string _name;

        public string Name
        {
            get { return _name; }
        }

        List<StarSystem> Adjescent;
        MissionNode _rootNode;

        public MissionNode RootNode
        {
            get { return _rootNode; }
        }

        public StarSystem(string Name)
        {
            _name = Name;
            Adjescent = new List<StarSystem>();
        }

        public void RemoveCompletedMission(Dictionary<StarSystem, Mission> Route)
        {
            Route.Add(this, _rootNode.Element);
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
    }
}
