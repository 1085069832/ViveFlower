﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class HandGrabController : MonoBehaviour
{
    Hand hand;
    [HideInInspector]
    public bool isGrab;//是否抓取
    [SerializeField] HandManager realHand;
    [SerializeField] Transform mirrirThumbBone3;

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<MirrorController>().isMirrorHand)
        {
            if (realHand)
                hand = realHand._hand;
        }
        else
        {
            hand = GetComponent<HandManager>()._hand;
        }

        if (hand != null)
        {
            float grabAngel = hand.PinchStrength;
            if (grabAngel > 0.7f)
            {
                isGrab = true;
            }
            else
            {
                isGrab = false;
            }
        }
    }

    /// <summary>
    /// 拇指尖端
    /// </summary>
    public Vector3 thumbTip
    {
        get
        {
            if (GetComponentInParent<MirrorController>().isMirrorHand)
            {
                return mirrirThumbBone3.position;
            }
            else
            {
                return hand.GetThumb().bones[3].Center.ToVector3();
            }
        }
    }

    /// <summary>
    /// 手速度
    /// </summary>
    public Vector3 handVelocity
    {
        get
        {
            return FingersVelocity();
        }
    }

    private Vector3 FingersVelocity()
    {
        if (hand != null)
        {
            Vector3 velocity = Vector3.zero;
            List<Finger> fingers = hand.Fingers;
            foreach (Finger finger in fingers)
            {
                velocity += finger.TipVelocity.ToVector3();
            }
            return (hand.PalmVelocity.ToVector3() + velocity) / 6;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
