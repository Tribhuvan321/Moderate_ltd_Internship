using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float speed = 5f; // Speed of the environment movement
    public Transform exit;
    public Transform stopPoint; // Point where the environment stops
    private bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            // Move the environment backward
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
    }

    public void StartParallax()
    {
        isMoving = true;
    }
}