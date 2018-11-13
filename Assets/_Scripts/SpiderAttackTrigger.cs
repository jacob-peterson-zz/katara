using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Scripts;

public class SpiderAttackTrigger : MonoBehaviour {

    private Animator spiderAnimator;
    private float lastPunchTime;

    private void Start()
    {
        spiderAnimator = GameObject.Find("Spider Enemy").GetComponent<Animator>();
        lastPunchTime = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" &&
            spiderAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "attack1" &&
            Time.time > lastPunchTime + 1)
        {
            CombatConstants.playerHealth -= CombatConstants.spiderAttackDamage;
            lastPunchTime = Time.time;
        }
    }
}
