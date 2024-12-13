using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrowRoomEnabler : MonoBehaviour
{
    public GameObject narrowRoom;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerCollider")
        {
            narrowRoom.SetActive(true);
        }
    }
}
