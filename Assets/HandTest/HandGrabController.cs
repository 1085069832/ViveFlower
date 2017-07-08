using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class HandGrabController : MonoBehaviour
{
    HandManager handManager;
    Hand hand;
    [HideInInspector]
    public bool isGrab;//是否抓取
    [HideInInspector]
    public bool startMesh;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (handManager != null)
        {
            hand = handManager._hand;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HandCenter")
        {
            handManager = other.GetComponent<HandManager>();
            startMesh = true;
        }
    }
}
