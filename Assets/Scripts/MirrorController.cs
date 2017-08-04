using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorController : MonoBehaviour
{
    public bool isMirrorHand;
    public bool isRight;
    [SerializeField] GameObject[] mirrorHandGroup;
    private void OnEnable()
    {
        if (!isMirrorHand && isRight)
        {
            for (int i = 0; i < mirrorHandGroup.Length; i++)
            {
                mirrorHandGroup[i].SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        if (!isMirrorHand && isRight)
        {
            for (int i = 0; i < mirrorHandGroup.Length; i++)
            {
                mirrorHandGroup[i].SetActive(false);
            }
        }
    }
}
