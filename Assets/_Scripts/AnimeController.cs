using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class AnimeController : MonoBehaviour {

    Animator animator;
    Rigidbody rBody;

    private bool isAi;

    private bool dodge;
    private bool attack;
    private bool block;

    // Use this for initialization
    void Start () {
        isAi = true;
        animator = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody>();
    }

    public void OnAnimatorMove()
		{
        if (Time.deltaTime > 0)
        {
            Vector3 v = animator.deltaPosition / Time.deltaTime;

            // we preserve the existing y part of the current velocity.
            v.y = rBody.velocity.y;
            rBody.velocity = v;
        }
    }

    public void processInput(float speed, float direction)
    {
        if (dodge)
        {
            print("dodge");
        }
        else if (attack)
        {
            print("attack");
        }
        else
        {
            if (block)
            {
                speed *= 0.5f;
            }

            animator.SetFloat("speed", speed, 0.1f, Time.deltaTime);
            animator.SetFloat("direction", direction, 0.1f, Time.deltaTime);
        }
    }

    public void isntAi()
    {
        isAi = false;
    }

    public void clearInput()
    {
        dodge = false;
        attack = false;
        block = false;
}

    public void setdodge()
    {
        dodge = true;
    }

    public void setAttack()
    {
        attack = true;
    }


    public void setBlock()
    {
        block = true;
    }
}
