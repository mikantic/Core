using Core.Tools;

namespace Core.Stats
{
    public interface IOffense
    {
        public Stat Punch { get; }

        public Stat Potency { get; }
    }

    public interface IDefense
    {
        public Stat Toughness { get; }

        public Stat Tolerance { get; }
    }
}