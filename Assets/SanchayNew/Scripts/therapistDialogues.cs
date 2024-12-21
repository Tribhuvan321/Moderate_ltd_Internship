using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class therapistDialogues : MonoBehaviour
{
    public string[] dialogues;
    public float timer = 0f;

    public bool interacted=false;
    public bool playerFollowed=true;
    public bool reachedPlayer=false;

    public bool playerAtMine=false;

    public float retryTimer, reachedDistance;

    public int currentDialogue;

    int ttsRequestCount = 5;
    int currentttsRequests = 0;

    Animator anim;
    navmeshTherapist therapistScript;
    TextToSpeech ttsScript;

    public bool vineConnected;

    private void Start()
    {
        anim = GetComponent<Animator>();
        therapistScript = GetComponent<navmeshTherapist>();
        ttsScript = GetComponent<TextToSpeech>();
    }

    private void Update()
    {

        if (Vector3.Distance(this.gameObject.transform.position, therapistScript.player.position)<= reachedDistance)
        {
            reachedPlayer = true;
        }

        else
        {
            reachedPlayer= false;
        }

        if (reachedPlayer && !interacted)
        {
            interacted = true;
            StartCoroutine(moveTonextPoint(2));
        }

        if (interacted && !playerFollowed && !anim.GetBool("isWalking") && therapistScript.reached)
        {
            timer += Time.deltaTime;
        }

        else if(interacted && playerFollowed && vineConnected)
        {
            timer = 0f;
            //switch to next position and next dialogue
            currentDialogue = 3;
        }

        else
        {
            timer = 0f;
        }

        if(timer>retryTimer)
        {
            timer = 0f;
            //go to player again and repeat the last dialogue
            StartCoroutine(moveTonextPoint(0, true));
        }
    }


    IEnumerator moveTonextPoint( int i)
    {
        if(currentttsRequests<ttsRequestCount)
        {
            ttsScript.startTTs(dialogues[currentDialogue]);
            currentttsRequests++;
        }
        yield return new WaitForSeconds(21f);
        therapistScript.setTransform(i);
        yield return new WaitForSeconds(4f);
        playerFollowed = false;
    }

    IEnumerator moveTonextPoint(int i, bool retryRequest)
    {
        /*playerFollowed = true;
        if (currentttsRequests < ttsRequestCount && reachedPlayer)
        {
            ttsScript.startTTs(dialogues[currentDialogue]);
            currentttsRequests++;
        }*/
        playerFollowed=true;
        //yield return new WaitForSeconds(1f);
        therapistScript.setTransform(i);
        yield return new WaitForSeconds(2f);
        interacted = false;
        //playerFollowed = false;
    }  

}
