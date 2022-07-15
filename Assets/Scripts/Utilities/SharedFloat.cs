using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class  SharedFloat : ScriptableObject
{
    public float initialValue, currentValue, initialMaxValue, maxValue;

    public event Action<ValueChangedEventArgs> ValueChangedEvent;
    public event Action<MaxValueChangedEventArgs> MaxValueChangedEvent;

    public float Value
    {
        get => currentValue;
        set
        {
            if (value > maxValue)
            {
                value = maxValue;
            }

            ValueChangedEventArgs args = new ValueChangedEventArgs();
            args.amountChanged = value - currentValue;
            args.newValue = value;
            
            currentValue = value;
            ValueChangedEvent?.Invoke(args);
        }
    }

    public float MaxValue
    {
        get => maxValue;
        set
        {
            if (currentValue > value)
            {
                currentValue = value;
            }
            
            MaxValueChangedEventArgs args = new MaxValueChangedEventArgs();
            args.amountChanged = value - maxValue;
            args.newValue = value;
            
            maxValue = value;
            MaxValueChangedEvent?.Invoke(args);
        }
    }

    private void Awake()
    {
        currentValue = initialValue;
        maxValue = initialMaxValue;
    }
}

public class ValueChangedEventArgs : EventArgs
{
    public float newValue;
    public float amountChanged;
}

public class MaxValueChangedEventArgs : EventArgs
{
    public float newValue;
    public float amountChanged;
}
