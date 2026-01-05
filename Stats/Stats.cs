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

    public interface IFlavors
    {
        public Stat Savory { get; }
        public Stat Spicy { get; }
        public Stat Salty { get; }
        public Stat Sweet { get; }
        public Stat Bitter { get; }
        public Stat Minty { get; }
        public Stat Sour { get; }
        public Stat Rotten { get; }

        /// <summary>
        /// default method of getting flavor stat
        /// </summary>
        /// <param name="flavor"></param>
        /// <returns></returns>
        public int GetFlavor(Flavor.Type flavor)
        {
            return flavor switch
            {
                Flavor.Type.Savory => Savory,
                Flavor.Type.Spicy => Spicy,
                Flavor.Type.Salty => Salty,
                Flavor.Type.Sweet => Sweet,
                Flavor.Type.Bitter => Bitter,
                Flavor.Type.Minty => Minty,
                Flavor.Type.Sour => Sour,
                Flavor.Type.Rotten => Rotten,
                // to do: figure out basic scaling
                _ => 0
            };
        }
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