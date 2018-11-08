using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdvancedPatrolScriptA : MonoBehaviour
{

    public float lookRad = 4f;
    public Transform[] points;

    private int destPoint = 0;
    private float walkSpeed = 1f;
    private float runSpeed = 2f;
    private float turnSpeed = 5f;
    public float attackDist = 1.3f;

    Transform target;
    NavMeshAgent agent;
    Animator anim;
    GameObject player;

    bool patrolling = true;
    bool playerSpotted = false;


    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        agent.speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);


        if (distance < 10)
        {

            if (distance < attackDist)
            {
                agent.destination = transform.position;

                Vector3 relativepos = player.transform.position - transform.position;
                //quaternion rotation = quaternion.lookrotation(relativepos, vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(relativepos), turnSpeed * Time.deltaTime);
                anim.SetTrigger("Attack");
            }
            else
            {
                agent.speed = runSpeed;
                agent.destination = player.transform.position;
                Vector3 relativePos = player.transform.position - transform.position;
                //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(relativePos), turnSpeed * Time.deltaTime);
            }
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
            agent.speed = walkSpeed;
        }
        else
        {
            agent.speed = walkSpeed;
        }

        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

}
