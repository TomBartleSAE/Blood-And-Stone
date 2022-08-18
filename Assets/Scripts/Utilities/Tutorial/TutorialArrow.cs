using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArrow : MonoBehaviour
{
    public float rotationSpeed = 45;
    public float bounceSpeed = 1f;
    public float bounceFactor = 1f;
    private float startingY;
    public void Start()
    {
        startingY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        float yPos = Mathf.Sin(Time.time * bounceSpeed) * bounceFactor;
        transform.position = new Vector3(transform.position.x, startingY + yPos, transform.position.z);
    }
}
