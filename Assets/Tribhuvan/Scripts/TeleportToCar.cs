using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToCar : MonoBehaviour
{
    public Transform player;
    public Transform carSeatPosition;
    public FadeManager fadeManager;
    public GameObject moveObj;
    public ParallaxEffect pe;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerCollider")
        {
            StartCoroutine(TeleportSequence());
        }
        pe.StartParallax();
    }

    private IEnumerator TeleportSequence()
    {
        fadeManager.FadeOut();
        yield return new WaitForSeconds(1f); // Wait for fade out animation
        player.position = carSeatPosition.localPosition;
        Vector3 rot = player.rotation.eulerAngles;
        rot.y += 90f;
        player.rotation = Quaternion.Euler(rot);
        yield return new WaitForSeconds(1f); // Wait before fading in
        moveObj.SetActive(false);
        fadeManager.FadeIn();
    }
}
