using Leap;
using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    LeapProvider leapProvider;
    Frame frame;
    Hand hand;

    private void Awake()
    {
        leapProvider = FindObjectOfType<LeapProvider>() as LeapProvider;
    }

    public Hand _hand
    {
        get
        {
            return hand;
        }
    }

    /// <summary>
    /// 获取触碰的手
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //伸出去的第一只手是0
        frame = leapProvider.CurrentFrame;
        if (transform.name == "Sphere_R")
        {
            if (frame.Hands[0].IsRight)
            {
                hand = frame.Hands[0];
            }
            else
            {
                hand = frame.Hands[1];
            }
        }
        else if (transform.name == "Sphere_L")
        {
            if (frame.Hands[0].IsLeft)
            {
                hand = frame.Hands[0];
            }
            else
            {
                hand = frame.Hands[1];
            }
        }
    }
}
