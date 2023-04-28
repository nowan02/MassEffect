using System;

namespace MassEffect
{
    public class InvalidRewardException : Exception
    {
        public readonly string MissionName;
        public readonly int Reward;
        public readonly int FileRow;

        public InvalidRewardException(string MissionName, int Reward, int FileRow) : base("Reward was negative.")
        {
            this.MissionName = MissionName;
            this.Reward = Reward;
            this.FileRow = FileRow;
        }
    }

    public class InvalidRiskException : Exception
    {
        public readonly string MissionName;
        public readonly int Risk;
        public readonly int FileRow;

        public InvalidRiskException(string MissionName, int Risk, int FileRow) : base("Risk wasn't between 1 and 10.")
        {
            this.MissionName = MissionName;
            this.Risk = Risk;
            this.FileRow = FileRow;
        }
    }

    public class FileErrorException : Exception
    {
        public readonly string FileName;
        public readonly int FileRow;

        public FileErrorException(string FileName, int FileRow) : base("The input file has incorrect data, which cannot be parsed.")
        {
            this.FileName = FileName;
            this.FileRow = FileRow;
        }
    }
}