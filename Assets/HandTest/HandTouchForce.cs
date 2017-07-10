using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTouchForce : MonoBehaviour
{
    public void AddForceForHand(Vector3 handVelocity)
    {
        GetComponent<Rigidbody>().AddForce(handVelocity, ForceMode.Force);
    }
}
