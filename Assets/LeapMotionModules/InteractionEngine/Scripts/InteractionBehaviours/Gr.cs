using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class Gr : InteractionBehaviour
{

    protected override void OnGraspBegin()
    {
        base.OnGraspBegin();
        print("开始抓取");
    }

    protected override void OnGraspEnd(Hand lastHand)
    {
        base.OnGraspEnd(lastHand);
        print("结束抓取");
    }
}
