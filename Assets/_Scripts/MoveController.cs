using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimeController))]
public class MoveController : MonoBehaviour {

    private AnimeController aCon; // A reference to the animation controller on the object
    private Vector3 move;
    private bool inAni;
    private bool sprint;

    // Use this for initialization
    void Start ()
    {
        //is requirement cant be null
        aCon = GetComponent<AnimeController>();
        aCon.isntAi();
        inAni = false;
	}

	// Update is called once per frame
	void Update ()
    {
        //dodge takes first priority
        if (!inAni && Input.GetKeyDown(KeyCode.Space))
        {
            aCon.setdodge();
            inAni = true;
        }

        //attack next
        if (!inAni && Input.GetKeyDown(KeyCode.Mouse0))
        {
            aCon.setAttack();
            inAni = true;
        }

        //finally non locking animations
        if (!inAni)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                sprint = true;
            }
            else if (Input.GetKey(KeyCode.Mouse1))
            {
                aCon.setBlock();
            }
        }
	}

    void FixedUpdate()
    {
        float speed = 0;
        float direction = 0;
        if (!inAni)
        {
            speed = Input.GetAxis("Vertical");
            direction = Input.GetAxis("Horizontal");

            if (sprint)
            {
                if (speed > 0)
                {
                    speed *= 4.05f;
                }
                else
                {
                    speed *= 2.93f;
                }

                direction *= 2.7f;
            }
            else
            {
                if (speed > 0)
                {
                    speed *= 1.65f;
                }
            }
        }

        //applies and clears input
        aCon.processInput(speed, direction);
        aCon.clearInput();
        inAni = false;
        sprint = false;
    }
}
