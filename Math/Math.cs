using System;
using System.Collections.Generic;
using System.Linq;
using Physics;
using UnityEngine;

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

        public static Vector3 Average(this IEnumerable<Vector3> vectors, Vector3 emptyResult = new Vector3())
        {
            int count = vectors.Count();
            if (count <= 0) return emptyResult;
            return vectors.Sum() / vectors.Count();
        }

        public static Vector3 Sum(this IEnumerable<Vector3> vectors, Vector3 emptyResult = new Vector3())
        {
            if (vectors.Count() <= 0) return emptyResult;
            return vectors.Aggregate((sum, vector) => sum += vector);
        }

        public static void Map<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable) action(item);
        }

        public static Vector3 RotateAround(this Vector3 vector, Vector3 axis, float angle)
        {
            return Quaternion.AngleAxis(angle, axis) * vector;
        }
    }
}