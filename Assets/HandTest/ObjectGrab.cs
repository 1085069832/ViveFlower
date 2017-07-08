using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 判断哪只手指碰到花
/// </summary>
public class ObjectGrab : MonoBehaviour
{
    public static ObjectGrab Instance;
    GrabbableObject grabbableObject;
    [HideInInspector]
    public bool canGrab;
    HandGrabController handGrabController;
    Transform thumbTip;
    FixedJoint fj;
    // List<Transform> fingers = new List<Transform>();

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
                fj = gameObject.AddComponent<FixedJoint>();
                fj.connectedBody = thumbTip.gameObject.GetComponent<Rigidbody>();
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

        if (rf && rf.fingerType == Leap.Finger.FingerType.TYPE_THUMB)
        {
            //是否是拇指
            print("canGrab true");
            handGrabController = GameObject.Find("Collider").GetComponent<HandGrabController>();
            canGrab = true;
            thumbTip = rf.bones[3];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RigidFinger rf = other.GetComponentInParent<RigidFinger>();

        if (rf && rf.fingerType == Leap.Finger.FingerType.TYPE_THUMB)
        {
            //是否是拇指
            canGrab = false;
            print("canGrab false");

        }
    }
}
