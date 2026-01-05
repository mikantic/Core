using Core.Stats;
using UnityEngine;

public class Tester : MonoBehaviour
    {
        public Defense Defense {get; } = new();
        public Offense Offense {get; } = new();
        public Spiceball Spiceball {get; } = new();

        public void Start()
        {
            Debug.Log(Calculations.Value(Defense, Spiceball, Offense));
        }
    }