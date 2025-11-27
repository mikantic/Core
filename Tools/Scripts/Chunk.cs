using System;

namespace Core.Tools
{
    public abstract class Chunk<T> : Range<T> where T : IComparable
    {
        /// <summary>
        /// get value as number of chunks
        /// </summary>
        public abstract int Chunks { get; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public Chunk(T value, T min, T max) : base(value, min, max)
        {

        }
    }
    
    /// <summary>
    /// default chunk class of double
    /// </summary>
    public class Chunk : Chunk<double>
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public Chunk(double value, double min, double max) : base(value, min, max) { }

        /// <inheritdoc/>
        public override double InverseLerp => Value.InverseLerp(Min, Max);

        /// <inheritdoc/>
        public override int Chunks => Value.Floor() - Min.Value.Floor();

        /// <inheritdoc/>
        public override double Distance => Max - Min;
    }
}