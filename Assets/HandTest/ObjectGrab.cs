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
    bool isTrigger;
    HandGrabController handGrabController;
    Transform thumbTip;
    FixedJoint fj;
    Transform handCenter;

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
                fj.connectedBody = handCenter.gameObject.GetComponent<Rigidbody>();
                GameObject.Find("FlowerManager").GetComponent<CreateFlower>().InstanceFlower();
            }
        }
        if (isTrigger && !handGrabController.isGrab)
        {
            //断开
            if (fj)
            {
                Destroy(fj);
                isTrigger = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        RigidFinger rf = other.GetComponentInParent<RigidFinger>();
        handCenter = other.GetComponentInParent<RigidHand>().transform.Find("palm").Find("Sphere");
        print(handCenter.name);
        handGrabController = other.GetComponentInParent<RigidHand>().GetComponentInChildren<HandGrabController>();
        isTrigger = true;
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
