using UnityEngine;
using System;

[CreateAssetMenu(fileName = "IntValue", menuName = "Scriptable Objects/IntValue")]
public class IntValue : ScriptableObject
{
    [SerializeField]
    private int initialValue;

    private int _value;
    public int value
    {
        set
        {
            if (_value != value)
            {
                _value = value;
                onValueChanged?.Invoke();
            }
        }
        get => _value;
    }

    public event Action onValueChanged;

    private void OnEnable()
    {
        value = initialValue;
    }
}
