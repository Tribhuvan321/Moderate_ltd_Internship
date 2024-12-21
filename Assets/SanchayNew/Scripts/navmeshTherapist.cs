using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmeshTherapist : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    public Transform player;
    public Transform startPos;
    public Transform vinePos;

    public Transform currentTransform;

    therapistDialogues therapistDialoguesScript;

    public bool reached;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        //player = GameObject.Find("Player").GetComponent<Transform>();

        //agent.SetDestination(player.position);
        InvokeRepeating("updateTarget", 0.5f,1.2f);

        currentTransform = player;

        therapistDialoguesScript = GetComponent<therapistDialogues>();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.velocity.magnitude!=0f)
        {
            anim.SetBool("isWalking",true);
        }

        else
        {
            anim.SetBool("isWalking", false);
        }

        if(Vector3.Magnitude(agent.velocity)==0f)
        {
            reached = true;
        }

        else
        {
            reached = false;
        }


    }

    private void OnAnimatorMove()
    {
        if (anim.GetBool("isWalking"))
        {
            agent.speed = (anim.deltaPosition / Time.deltaTime).magnitude;
        }
    }

    void updateTarget()
    {
        agent.SetDestination(currentTransform.position);
    }

    public void setTransform(int i)
    {
        switch (i)
            {
            case 0:
                currentTransform = player;
                break;

            case 1:
                currentTransform = startPos; 
                break;

            case 2:
                currentTransform = vinePos;
                break;
        }
    }

    void updateDialogue()
    {
        if (agent.remainingDistance == 0)
        {
            if (currentTransform == player)
            {
                therapistDialoguesScript.currentDialogue = 0;
            }

            else if(currentTransform == vinePos) 
            {
                therapistDialoguesScript.currentDialogue = 0;
            }
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        if(other.name=="Player")
        {
            //reached player
            reachedPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            //reached player
            reachedPlayer = false;
        }
    }*/
}
