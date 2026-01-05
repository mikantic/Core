using Core.Tools;

namespace Core.Stats
{
    public interface IHealth
    {
        public Magnitude Health { get; }
    }

    public interface IMana
    {
        public Magnitude Mana { get; }
    }

    public interface IFlavor
    {
        public Stat Savory { get; }
        public Stat Spicy { get; }
        public Stat Salty { get; }
        public Stat Sweet { get; }
        public Stat Bitter { get; }
        public Stat Minty { get; }
        public Stat Sour { get; }
        public Stat Rotten { get; }
    }

    public interface IOffense
    {
        public double Punch { get; }
        public double Potency { get; }
    }

    public interface IDefense
    {
        public double Tolerance { get; }
        public double Toughness { get; }
    }
}