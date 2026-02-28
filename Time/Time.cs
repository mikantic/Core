namespace Core
{
    public static class Time
    {
        public static float Scale = 1f;
        public static float Delta
        {
            get => UnityEngine.Time.deltaTime * Scale;
        }

        public static float FixedDelta => UnityEngine.Time.fixedDeltaTime;
    }
}