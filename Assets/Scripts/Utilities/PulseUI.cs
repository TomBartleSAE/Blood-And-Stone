using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseUI : MonoBehaviour
{
    private RectTransform rectTransform;

    public bool isPulsing;
    public bool IsPulsing
    {
        get => isPulsing;
        set
        {
            isPulsing = value;
            
            if (value == false)
            {
                rectTransform.localScale = Vector3.one;
            }
        }
    }

    public float scaleMagnitude = 0.5f;
    public float offset = 1.5f;
    public float speed = 5f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isPulsing)
        {
            float scale = Mathf.Sin(Time.time * speed) * scaleMagnitude + offset;
            rectTransform.localScale = new Vector3(scale, scale, 1);
        }
    }
}
