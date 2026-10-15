using Core.Properties;
using UnityEngine;

public class PropertyTesting : MonoBehaviour
{
    [SerializeField] protected PropertyList<bool> _list = new();

    [SerializeField] protected Range<double> _range = new(5, 10);

    [SerializeField] protected Positive<int> _whole = new(5);

}