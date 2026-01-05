using Core.Stats;
using Core.Tools;

namespace Core.Damage
{
    /// <summary>
    /// type of damage linking defense and offense
    /// </summary>
    public enum Type
    {
        Physical, Magical
    }

    public interface IArmor
    {
        /// <summary>
        /// optional method to get armor value added to defense in damage calc
        /// </summary>
        /// <returns></returns>
        public double GetArmor()
        {
            return 5;
        }

    }

    /// <summary>
    /// anything that can receive damage
    /// </summary>
    public interface IDefend : IDefense, IHealth, IArmor
    {
        /// <summary>
        /// default method of getting stat to defend with
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public double GetDefense(Type source)
        {
            return source switch
            {
                Type.Physical => Toughness,
                Type.Magical => Tolerance,
                _ => 0  
            };
        }

        /// <summary>
        /// modifies health with damage received
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="offense"></param>
        public void ReceiveDamage(IDamage damage, IAttack offense)
        {
            Health.Value -= this.CalculateDamage(damage, offense);
        }
    }

    /// <summary>
    /// anything that can cause damage
    /// </summary>
    public interface IAttack : IOffense, IFlavor
    {
        /// <summary>
        /// default method of getting offensive stat
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public double GetOffense(Type source)
        {
            return source switch
            {
                Type.Physical => Punch,
                Type.Magical => Potency,
                _ => 0  
            };
        }

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
        public Type Type { get; }

        /// <summary>
        /// related flavor
        /// </summary>
        public Flavor.Type Flavor { get; }

        /// <summary>
        /// scaling with flavor
        /// </summary>
        public Magnitude Proficiency { get; }
    }

    public static class DamageExtensions
    {
        /// <summary>
        /// get proficiency factpr
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="offense"></param>
        /// <returns></returns>
        public static double GetBoost(IDamage damage, IAttack offense)
        {
            return 1 + offense.GetFlavor(damage.Flavor) * damage.Proficiency;
        }

        /// <summary>
        /// get offense defense factor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="defense"></param>
        /// <param name="offense"></param>
        /// <returns></returns>
        public static double GetModification(Type type, IDefend defense, IAttack offense)
        {
            return (1 + offense.GetOffense(type)) / (1 + defense.GetDefense(type) + defense.GetArmor());
        }

        /// <summary>
        /// calculate damage dealt
        /// </summary>
        /// <param name="defense"></param>
        /// <param name="damage"></param>
        /// <param name="offense"></param>
        /// <returns></returns>
        public static double CalculateDamage(this IDefend defense, IDamage damage, IAttack offense)
        {
            return damage.Base * GetModification(damage.Type, defense, offense) * GetBoost(damage, offense);
        }

        /// <summary>
        /// extension with damage as this
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="defense"></param>
        /// <param name="offense"></param>
        /// <returns></returns>
        public static double CalculateDamage(this IDamage damage, IDefend defense, IAttack offense) =>
            defense.CalculateDamage(damage, offense);

        /// <summary>
        /// extension with offense as this
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="defense"></param>
        /// <param name="offense"></param>
        /// <returns></returns>
        public static double CalculateDamage(this IAttack offense, IDefend defense, IDamage damage) =>
            defense.CalculateDamage(damage, offense);
    }
}