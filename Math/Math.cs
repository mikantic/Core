using System;
using System.Collections.Generic;
using System.Linq;
using Physics;
using UnityEngine;

#nullable enable

namespace Core.Numerics
{
    public static class Math
    {
        public const double DOT_135 = -0.71;
        public const double DOT_90 = 0;
        public const double DOT_45 = 0.71;

        public static float Dot(this Vector3 vector, Vector3 other)
        {
            return Vector3.Dot(vector.normalized, other.normalized);
        }

        public static float InDirection(this Vector3 vector, Vector3 other)
        {
            return Vector3.Dot(vector, other.normalized);
        }

        public static Vector3 Average(this IEnumerable<Vector3> vectors)
        {
            return vectors.Sum() / vectors.Count();
        }

        public static Vector3 Average(this IEnumerable<Vector3> vectors, Vector3 emptyResult)
        {
            int count = vectors.Count();
            if (count <= 0) return emptyResult;
            return vectors.Average();
        }

        public static Vector3 Sum(this IEnumerable<Vector3> vectors)
        {
            return vectors.Aggregate((sum, vector) => sum += vector);
        }

        public static float Maximum(this IEnumerable<float> values)
        {
            return values.Max();
        }

        public static float Maximum(this IEnumerable<float> values, float emptyResult)
        {
            if (values.Count() <= 0) return emptyResult;
            return values.Max();
        }

        public static bool InLayerMask(this int layer, LayerMask layerMask)
        {
            return (layerMask.value & (1 << layer)) != 0;
        }

        public static float WrapDegrees(this float value)
        {
            return (value % 360f + 360f) % 360f;
        }
    }
}