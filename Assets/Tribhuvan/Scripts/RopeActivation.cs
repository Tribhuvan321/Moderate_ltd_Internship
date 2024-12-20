using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeActivation : MonoBehaviour
{
    public Transform player;
    public Transform minePos;
    public Transform ladderPlatformPos;
    public FadeManager fadeManager;

    public void CallTheTeleportSequence()
    {
        StartCoroutine(TeleportSequence());
    }

    private IEnumerator TeleportSequence()
    {
        fadeManager.FadeOut();
        yield return new WaitForSeconds(1f); // Wait for fade out animation
        player.position = minePos.localPosition;
        Vector3 rot = player.rotation.eulerAngles;
        rot.y += 90f;
        player.rotation = Quaternion.Euler(rot);
        yield return new WaitForSeconds(1f); // Wait before fading in
        fadeManager.FadeIn();

        ladderPlatformPos.position = ladderPlatformPos.position + minePos.position;
        minePos.position = ladderPlatformPos.position - minePos.position;
        ladderPlatformPos.position = ladderPlatformPos.position - minePos.position;
    }
}
