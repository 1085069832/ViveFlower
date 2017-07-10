using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class HandGrabController : MonoBehaviour
{
    Hand hand;
    [HideInInspector]
    public bool isGrab;//是否抓取
    // Update is called once per frame
    void Update()
    {
        hand = transform.GetComponent<HandManager>()._hand;

        if (hand != null)
        {
            if (hand.PinchStrength > 0.8f)
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
            return hand.Fingers[0].TipPosition.ToVector3();
        }
    }
}
