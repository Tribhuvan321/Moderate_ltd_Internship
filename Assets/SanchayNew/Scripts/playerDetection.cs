using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetection : MonoBehaviour
{

    public therapistDialogues therapist;

    private void OnTriggerStay(Collider other)
    {
        if (other.name=="Player")
        {
            //player reached the mine
            therapist.playerAtMine = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            //player reached the mine
            therapist.playerAtMine = false;
        }
    }
}
