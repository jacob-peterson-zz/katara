using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontGateController : MonoBehaviour
{

    private Animator doorAnim;
    private bool open = false;
    
	void Start ()
	{
	    doorAnim = GetComponent<Animator>();
	}


    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            open = !open;
            doorAnim.SetBool("Open", open);
        }
    }
}
