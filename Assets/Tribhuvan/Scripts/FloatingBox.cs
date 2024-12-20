using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBox : MonoBehaviour
{
    public float floatAmplitude = 0.5f; // Range of floating motion
    public float floatSpeed = 1f; // Speed of floating motion
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + floatAmplitude * Mathf.Sin(Time.time * floatSpeed);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}