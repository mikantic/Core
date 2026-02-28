using System.Collections.Generic;
using System.Linq;
using Core.Stats;
using Core.Tools;

namespace Core.Damage
{
    public enum Type
    {
        Physical, Magical    
    }

    public interface IEffect
    {
        public Stat Damage { get; }
        public Dictionary<Type, Ratio> Weights { get; }
        public Dictionary<Type, Magnitude> Scaling { get; }
        public Dictionary<Status.Type, Magnitude> Status { get; }
    }

    public static class Math
    {
        public static double GetDefense(this IDefense defense, Type type)
        {
            return type switch
            {
                Type.Physical => defense.Toughness,
                Type.Magical => defense.Tolerance,
                _ => 0  
            };
        }

        public static double GetOffense(this IOffense offense, Type type)
        {
            return type switch
            {
                Type.Physical => offense.Punch,
                Type.Magical => offense.Potency,
                _ => 0  
            };
        }

        public static double GetDefense(this IDefense defense, IEffect effect)
        {
            return effect.Weights.Sum(weight => defense.GetDefense(weight.Key) * weight.Value);
        }

        public static double GetOffense(this IOffense offense, IEffect effect)
        {
            return effect.Scaling.Sum(scaling => offense.GetOffense(scaling.Key) * scaling.Value);
        }

        public static double GetDamage(this IEffect effect, IOffense offense, IDefense defense)
        {
            return effect.Damage * System.Math.Sqrt(offense.GetOffense(effect) / (1 + defense.GetDefense(effect)));
        }
    }

}