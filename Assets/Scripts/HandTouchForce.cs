using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTouchForce : MonoBehaviour
{
    [SerializeField] float force = 10;

    public void AddForceForHand(Vector3 handVelocity)
    {
        GetComponent<Rigidbody>().AddForce(handVelocity * GetComponent<Rigidbody>().mass * force, ForceMode.Force);
    }
}
