using System;
using Core.Tools;
using UnityEngine;

namespace Core.Stats
{
    public interface IOffense<T> where T : Enum
    {
        public double GetOffense(T type);
    }

    public interface IDefense<T> where T : Enum
    {
        public double GetDefense(T type);
    }

    public interface IArmor<T> where T : Enum
    {
        public Magnitude GetArmor(T value);
    }

    public interface IModify<T> where T : Enum
    {
        public Magnitude GetModifications(T value);
    }

    public interface IFlavor
    {
        public Flavor.Type Flavor { get; }
    }

    public interface IProficiency : IFlavor
    {
        public double Proficiency { get; }
    }

    public interface IDefend : IDefense<Damage.Type>, IDefense<Status.Type>
    {
        
    }

    public interface IAttack : IOffense<Damage.Type>, IOffense<Status.Type>, IOffense<Flavor.Type>
    {
        
    }

    public interface IDamage<T> where T : Enum
    {
        public T Type { get; }
        public double Value { get; }
    }

    public interface IValue<T> where T : Enum
    {
        public T Type { get; }
        public double Value { get; }
    }


    public static class Calculations
    {
        /// <summary>
        /// calculates a potential damage value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defense"></param>
        /// <param name="damage"></param>
        /// <param name="offense"></param>
        /// <returns></returns>
        public static double Value<T>(IDefense<T> defense, IDamage<T> damage, IOffense<T> offense) where T : Enum
        {
            double baseDamage = damage.Value;
            Debug.Log(baseDamage);
            double proficiency = damage.Proficiency(offense);
            Debug.Log(proficiency);
            double factor = damage.Factor(defense, offense);
            Debug.Log(factor);
            return baseDamage * proficiency / factor;
        }

        /// <summary>
        /// tries to get modifications to base damage for a type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="damage"></param>
        /// <param name="offense"></param>
        /// <returns></returns>
        public static double Modifications<T>(this IDamage<T> damage, object offense) where T : Enum
        {
            if (offense is not IModify<T> modifier) return damage.Value;
            return modifier.GetModifications(damage.Type) + damage.Value;
        }

        /// <summary>
        /// tries to get a proficiency value from a source with a matching type, find better way to do this
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offense"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static double Proficiency(this object damage, object offense)
        {
            if (damage is not IProficiency tool) return 1;
            if (offense is not IOffense<Flavor.Type> attacker) return 1;
            return 1 + attacker.GetOffense(tool.Flavor) * tool.Proficiency;
        }

        /// <summary>
        /// tries to get an armor value from the defense of matching type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="damage"></param>
        /// <param name="defense"></param>
        /// <returns></returns>
        public static double Armor<T>(this IDamage<T> damage, object defense) where T : Enum
        {
            if (defense is not IArmor<T> armored) return 0;
            return armored.GetArmor(damage.Type);
        }

        /// <summary>
        /// gets a logorthmic scale based on defense and offense values for a type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="defense"></param>
        /// <param name="offense"></param>
        /// <returns></returns>
        public static double Factor<T>(this IDamage<T> damage, IDefense<T> defense, IOffense<T> offense) where T : Enum
        {
            return 1 + ((defense.GetDefense(damage.Type) + damage.Armor(defense)) / (1 + offense.GetOffense(damage.Type)));
        }

    }

    public class Spiceball : IDamage<Damage.Type>, IProficiency
    {
        public Damage.Type Type { get => Damage.Type.Magical; }
        public double Value { get => 30; }
        public Flavor.Type Flavor { get => Core.Flavor.Type.Spicy; }
        public double Proficiency { get => 0.05; }
    }

    public class Offense : IAttack
    {
        public double GetOffense(Damage.Type type)
        {
            return 10;
        }

        public double GetOffense(Flavor.Type type)
        {
            return 3;
        }

        public double GetOffense(Status.Type type)
        {
            return 0;
        }
    }

    public class Defense : IDefend
    {
        public double GetDefense(Damage.Type type)
        {
            return 7;
        }

        public double GetDefense(Status.Type type)
        {
            return 0;
        }
    }
}