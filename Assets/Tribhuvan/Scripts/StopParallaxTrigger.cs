using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopParallaxTrigger : MonoBehaviour
{
    public ParallaxEffect parallaxEffect; // Reference to the ParallaxEffect script
    public GameObject wormHole;
    public GameObject plane;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the "Player" tag
        if (other.name == "PlayerCollider")
        {
            // Stop the parallax movement
            parallaxEffect.enabled = false;
            Debug.Log("Parallax Effect Stopped");
            wormHole.SetActive(true);
            plane.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}

