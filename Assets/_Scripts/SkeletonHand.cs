﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHand : MonoBehaviour {
    private Animator skeletonAnimator;
    private float lastPunchTime;

    private void Start()
    {
        skeletonAnimator = GameObject.Find("Skeleton Enemy").GetComponent<Animator>();
        lastPunchTime = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" &&
            skeletonAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Punching" &&
            Time.time > lastPunchTime + 1)
        {
            PlayerInventory.currentHealth -= 10;
            lastPunchTime = Time.time;
        }
    }
}