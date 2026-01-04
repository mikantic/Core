using Core.Flavors;
using Core.Stats;
using Core.Tools;

namespace Core.Damage
{
    /// <summary>
    /// type of damage linking defense and offense
    /// </summary>
    public enum Damage
    {
        Physical, Magical
    }

    /// <summary>
    /// anything that can receive damage
    /// </summary>
    public interface IDefense : Stats.IDefense, IHealth
    {
        /// <summary>
        /// default method of getting stat to defend with
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public double GetDefense(Damage source)
        {
            return source switch
            {
                Damage.Physical => Toughness,
                Damage.Magical => Tolerance,
                _ => 0  
            };
        }

        /// <summary>
        /// optional method to get armor value added to defense in damage calc
        /// </summary>
        /// <returns></returns>
        public double GetArmor()
        {
            return 5;
        }

        /// <summary>
        /// modifies health with damage received
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="offense"></param>
        public void ReceiveDamage(IDamage damage, IOffense offense)
        {
            Health.Value -= this.CalculateDamage(damage, offense);
        }
    }

    /// <summary>
    /// anything that can cause damage
    /// </summary>
    public interface IOffense : Stats.IOffense, IFlavor
    {
        /// <summary>
        /// default method of getting offensive stat
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public double GetOffense(Damage source)
        {
            return source switch
            {
                Damage.Physical => Punch,
                Damage.Magical => Potency,
                _ => 0  
            };
        }

        /// <summary>
        /// default method of getting flavor stat
        /// </summary>
        /// <param name="flavor"></param>
        /// <returns></returns>
        public int GetFlavor(Flavor flavor)
        {
            return flavor switch
            {
                Flavor.Savory => Savory,
                Flavor.Spicy => Spicy,
                Flavor.Salty => Salty,
                Flavor.Sweet => Sweet,
                Flavor.Bitter => Bitter,
                Flavor.Minty => Minty,
                Flavor.Sour => Sour,
                Flavor.Rotten => Rotten,
                _ => 0
            };
        }
    }

    /// <summary>
    /// interface for a damage source
    /// </summary>
    public interface IDamage
    {
        /// <summary>
        /// base damage
        /// </summary>
        public Stat Base { get; }

        /// <summary>
        /// type of damage
        /// </summary>
        public Damage Type { get; }

        /// <summary>
        /// related flavor
        /// </summary>
        public Flavor Flavor { get; }

        /// <summary>
        /// scaling with flavor
        /// </summary>
        public Magnitude Boost { get; }
    }

    public static class DamageExtensions
    {
        /// <summary>
        /// calculate damage dealt
        /// </summary>
        /// <param name="defense"></param>
        /// <param name="damage"></param>
        /// <param name="offense"></param>
        /// <returns></returns>
        public static double CalculateDamage(this IDefense defense, IDamage damage, IOffense offense)
        {
            return damage.Base 
                * (offense.GetOffense(damage.Type) / (defense.GetDefense(damage.Type) + defense.GetArmor()))
                * (1 + offense.GetFlavor(damage.Flavor) * damage.Boost);
        }

        /// <summary>
        /// extension with damage as this
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="defense"></param>
        /// <param name="offense"></param>
        /// <returns></returns>
        public static double CalculateDamage(this IDamage damage, IDefense defense, IOffense offense) =>
            CalculateDamage(defense, damage, offense);

        /// <summary>
        /// extension with offense as this
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="defense"></param>
        /// <param name="offense"></param>
        /// <returns></returns>
        public static double CalculateDamage(this IOffense offense, IDefense defense, IDamage damage) =>
            CalculateDamage(defense, damage, offense);
    }
}