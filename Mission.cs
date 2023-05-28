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
        int _risk;
        int _reward;
        string _name;
        string _description;

        public float Ratio
        {
            get { return _reward / _risk; }
        }

        public int RiskFactor
        {
            get { return _risk; }
        }

        public int Reward
        {
            get { return _reward; }
        }

        public string Name
        {
            get { return _name; }
        }

        public Mission(string Name, int Risk, int Reward, string Description)
        {
            _risk = Risk;
            _reward = Reward;
            _name = Name;
            _description = Description;
        }

        public override string ToString()
        {
            return $"   {_name}\n    {_description}\n     Risk: {_risk}\n    Reward: {_reward}\n";
        }
    }
}