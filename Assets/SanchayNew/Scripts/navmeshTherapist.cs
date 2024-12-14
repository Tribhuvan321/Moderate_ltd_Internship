using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmeshTherapist : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    Transform player;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Transform>();

        //agent.SetDestination(player.position);
        InvokeRepeating("updateTarget", 0.5f,1.2f);
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
        agent.SetDestination(player.position);
    }
}
