using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    [HideInInspector]
    public bool isMesh;
    private void OnTriggerEnter(Collider other)
    {
        isMesh = true;
    }
}
