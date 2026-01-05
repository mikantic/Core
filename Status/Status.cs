using Core.Damage;
using Core.Stats;
using Core.Tools;

namespace Core.Status
{
    public enum Type
    {
        Acid, Stun, Bleed, Rot
    }

    public interface IStatus
    {
        /// <summary>
        /// base status buildup
        /// </summary>
        public Stat Base { get; }

        /// <summary>
        /// status enum
        /// </summary>
        public Type Type { get; }
    }

    public interface IReceiveStatus : IDefense, IHealth
    {
        /// <summary>
        /// acid buildup
        /// </summary>
        public StatusMeter Acid { get; }

        /// <summary>
        /// stun buildup
        /// </summary>
        public StatusMeter Stun { get; }

        /// <summary>
        /// bleed buildup
        /// </summary>
        public StatusMeter Bleed { get; }

        /// <summary>
        /// rot buildup
        /// </summary>
        public StatusMeter Rot { get; }

        /// <summary>
        /// gets the defense value for a status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public double GetDefense(Type status)
        {
            return (Toughness + Tolerance);
        }

        /// <summary>
        /// gets the status value from a type
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public StatusMeter GetMeter(Type status)
        {
            return status switch
            {
                Type.Acid => Acid,
                Type.Stun => Stun,
                Type.Bleed => Bleed,
                Type.Rot => Rot,
                _ => null
            };
        }

        /// <summary>
        /// tries to update the status from a source
        /// </summary>
        /// <param name="status"></param>
        /// <param name="offense"></param>
        public void ReceiveStatus(IStatus status, IApplyStatus offense)
        {
            GetMeter(status.Type).Value += this.GetBuildup(status, offense);
        }
    }

    public interface IApplyStatus : IOffense
    {
        /// <summary>
        /// default method of getting offensive stat
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public double GetOffense(Type status)
        {
            return (Punch + Potency);
        }

        /// <summary>
        /// gets the additional status from other sources, rename
        /// </summary>
        public double StatusBoost { get; }
    }

    public static class StatusExtensions
    {
        /// <summary>
        /// gets the factor of defense and offense on status
        /// </summary>
        /// <param name="defense"></param>
        /// <param name="status"></param>
        /// <param name="offense"></param>
        /// <returns></returns>
        public static double GetModification(IReceiveStatus defense, IStatus status, IApplyStatus offense)
        {
            return 1 + defense.GetDefense(status.Type) / (1 + offense.GetOffense(status.Type));
        }

        /// <summary>
        /// gets the status applied to defense from offense with a status
        /// </summary>
        /// <param name="defense"></param>
        /// <param name="status"></param>
        /// <param name="offense"></param>
        /// <returns></returns>
        public static double GetBuildup(this IReceiveStatus defense, IStatus status, IApplyStatus offense)
        {
            return (status.Base + offense.StatusBoost) * GetModification(defense, status, offense);
        }

        /// <summary>
        /// extension with different this
        /// </summary>
        /// <param name="status"></param>
        /// <param name="defense"></param>
        /// <param name="offense"></param>
        /// <returns></returns>
        public static double GetBuildup(this IStatus status, IReceiveStatus defense, IApplyStatus offense) =>
            defense.GetBuildup(status, offense);
        
        /// <summary>
        /// extension with different this
        /// </summary>
        /// <param name="status"></param>
        /// <param name="defense"></param>
        /// <param name="offense"></param>
        /// <returns></returns>
        public static double GetBuildup(this IApplyStatus offense, IStatus status, IReceiveStatus defense) =>
            defense.GetBuildup(status, offense);


        public static void SetupAcid(IHealth health, StatusMeter acid)
        {
            // acid on value changed = if active health -= 1% of max health
            // acid on active changed = if active then start coroutine for 30s to decrease acid value by 1/30 a second
        }
    }


    /// <summary>
    /// tbd if this should be expanded or not, might be able have some linker helper that links events here with cooldowns and modifying health, mana, etc
    /// </summary>
    public abstract class StatusMeter : Meter
    {
        /// <summary>
        /// active boolean
        /// </summary>
        public Observable<bool> Active { get; }

        /// <summary>
        /// check if active and if so dont let value increase
        /// </summary>
        /// <param name="value"></param>
        protected override void InternalValidation(ref double value)
        {
            if (Active && value > Value) value = Value;
            base.InternalValidation(ref value);
        }

        /// <summary>
        /// check if value is at max, then set active true
        /// </summary>
        protected override void InternalResponse()
        {
            base.InternalResponse();
            if (Value >= Max) Active.Value = true;
        }

        /// <summary>
        /// setup active boolean
        /// </summary>
        /// <param name="value"></param>
        public StatusMeter(double value) : base(value)
        {
            Active = new(false);
        }
    }
}