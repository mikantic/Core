using UnityEngine;

namespace Core.Numerics
{
    public static class Math
    {
        public static Vector3 InDirection(this Vector3 vector, Vector3 other)
        {
            return vector * Vector3.Dot(vector, other);
        }
    }
}