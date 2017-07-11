using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    public static ObjectGrab Instance;
    GrabbableObject grabbableObject;
    [HideInInspector]
    public bool canGrab;
    HandGrabController handGrabController;
    Transform thumbTip;
    FixedJoint fj;
    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        grabbableObject = GetComponent<GrabbableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canGrab && handGrabController.isGrab)
        {
            //抓取
            if (!fj)
            {
                grabbableObject.OnGrab();
                transform.position = handGrabController.thumbTip;
                fj = gameObject.AddComponent<FixedJoint>();
                fj.connectedBody = thumbTip.gameObject.GetComponent<Rigidbody>();
                GameObject.Find("FlowerManager").GetComponent<CreateFlower>().InstanceFlower();
            }
        }
        else
        {
            //断开
            if (fj)
                Destroy(fj);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        RigidFinger rf = other.GetComponentInParent<RigidFinger>();
        handGrabController = other.GetComponentInParent<RigidHand>().GetComponentInChildren<HandGrabController>();
        if (rf && rf.fingerType == Leap.Finger.FingerType.TYPE_THUMB)
        {
            //是否是拇指
            thumbTip = rf.bones[3];
            canGrab = true;
        }
        if (!canGrab)
            GetComponent<HandTouchForce>().AddForceForHand(handGrabController.handVelocity);
    }

    private void OnTriggerExit(Collider other)
    {
        RigidFinger rf = other.GetComponentInParent<RigidFinger>();

        if (rf && rf.fingerType == Leap.Finger.FingerType.TYPE_THUMB)
        {
            //是否是拇指
            canGrab = false;
        }
    }
}
