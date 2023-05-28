namespace MassEffect
{
    public class MissionNode
    {
        Mission _element;
        MissionNode _nextElement;

        public Mission Element
        {
            get { return _element; }
        }

        public MissionNode NextElement
        {
            get { return _nextElement; }
        }

        public MissionNode(Mission Element)
        {
            this._element = Element;
        }

        public void Add(MissionNode Element)
        {
            if (_nextElement != null)
            {
                _nextElement.Add(Element);
            }
            else
            {
                _nextElement = Element;
            }
        }

        public string PrintAllMissions()
        {
            string Output = "";
            MissionNode n = this;
            while (n.NextElement != null)
            {
                Output += n._element.ToString();
                n = n._nextElement;
            }

            return Output;
        }
    }

    public class Mission
    {
        int Risk;
        int Reward;
        string Name;
        string Description;

        public float Ratio
        {
            get { return Reward / Risk; }
        }

        public int RiskFactor
        {
            get { return Risk; }
        }

        public Mission(string Name, int Risk, int Reward, string Description)
        {
            this.Risk = Risk;
            this.Reward = Reward;
            this.Name = Name;
            this.Description = Description;
        }

        public override string ToString()
        {
            return $"   {Name}\n    {Description}\n     Risk: {Risk}\n    Reward: {Reward}\n";
        }
    }
}