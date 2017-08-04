using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 这个东西。。还是不要理解了，没有复用的意义。。
/// 之后做镜像如果有能力的话再写一个吧。。
/// 旋转实在太头疼
/// </summary>
public class MirrorHand : MonoBehaviour
{

    [SerializeField]
    List<Transform> selfHandBone;
    [SerializeField]
    List<Transform> copyHandBone;
    [SerializeField]
    Transform mirrorTrans;
    [SerializeField]
    bool isRigidHand;
    [SerializeField]
    bool isSkinHand;
    [SerializeField]
    bool isRight;
    [SerializeField]
    Transform skinPalm;
    [SerializeField]
    Transform mirrorPalm;
    [SerializeField]
    Transform thisPalm;
    [SerializeField]
    MirrorHand meshHand;
    [SerializeField]
    Transform mirrorForearm, thisForearm;
    public Vector3 deviation;
    [SerializeField]
    Transform copyPalm;
    [SerializeField]
    Transform handModel;
    private void Awake()
    {
    }
    private void Update()
    {
        Copy();
        MirrorPos();
    }
    void Copy()
    {
        if (isSkinHand)
        {
            selfHandBone[0].localPosition = -copyHandBone[0].localPosition;
            Vector3 pos = selfHandBone[0].position;
            if (!thisForearm)
            {
                pos = 2 * selfHandBone[0].parent.position - selfHandBone[0].position;
            }
            selfHandBone[0].position = pos;
            Vector3 euler = copyHandBone[0].localEulerAngles;
            selfHandBone[0].localEulerAngles = euler;

            if (thisForearm)
            {
                selfHandBone[0].eulerAngles = copyPalm.eulerAngles;
                //Vector3 fPos = -mirrorForearm.position + 2 * handModel.position;
                ////fPos.y = -fPos.y;
                //thisForearm.position = fPos;
                //Vector3 fEular = mirrorForearm.eulerAngles;
                Vector3 oCamera = handModel.position - mirrorTrans.position;
                oCamera.y = 0;
                //fEular = Quaternion.AngleAxis(180, oCamera) * fEular;
                //thisForearm.eulerAngles = fEular;
                //thisForearm.localEulerAngles = mirrorForearm.localEulerAngles;
                thisForearm.forward = (thisPalm.position - thisForearm.position).normalized;
                //print((mirrorPalm.position - mirrorForearm.position).normalized + " " + mirrorForearm.forward);
                Vector3 oFPos = mirrorForearm.position - mirrorTrans.position;
                oFPos.y = -oFPos.y;
                thisForearm.position = Quaternion.AngleAxis(180, oCamera) * oFPos + mirrorTrans.position;
            }
            //print(selfHandBone[0].localEulerAngles + " " + eular);
            if (selfHandBone.Count == copyHandBone.Count)
            {
                for (int i = 1; i < selfHandBone.Count; i++)
                {
                    CopyBoneTransform(selfHandBone[i], copyHandBone[i]);
                }
            }
            //if (thisForearm)
            //{
            //    thisForearm.localPosition = -mirrorForearm.localPosition;
            //    Vector3 fPos = thisForearm.position;
            //    fPos = 2 * thisForearm.parent.position - thisForearm.position;
            //    thisForearm.position = pos;
            //    thisForearm.forward = thisPalm.position - thisForearm.position;
            //}
        }
        else
        {
            if (selfHandBone.Count == copyHandBone.Count)
            {
                for (int i = 0; i < selfHandBone.Count; i++)
                {
                    CopyBoneTransform(selfHandBone[i], copyHandBone[i]);
                }
            }
        }
    }
    void CopyBoneTransform(Transform selfHandBone, Transform copyHandBone)
    {
        if (isSkinHand)
        {
            if (selfHandBone.parent == skinPalm)
            {
                selfHandBone.localPosition = copyHandBone.localPosition;
                selfHandBone.localEulerAngles = copyHandBone.localEulerAngles;
            }
            else
            {
                selfHandBone.localPosition = -copyHandBone.localPosition;
                selfHandBone.localEulerAngles = copyHandBone.localEulerAngles;
            }
        }
        else
        {
            selfHandBone.localPosition = -copyHandBone.localPosition;
            selfHandBone.localEulerAngles = copyHandBone.localEulerAngles;
        }
    }
    void MirrorPos()
    {//transform.position= mirrorTrans.position-( mirrorTrans.position+mirrorHand.position-2*Camera.main.transform.position

        if (!isRigidHand)
        {
            Vector3 m = mirrorTrans.position;
            Vector3 o = Camera.main.transform.position;
            Vector3 om = m - o;
            Vector3 a = thisPalm.position;
            Vector3 oa = a - o;
            Vector3 ob = Quaternion.AngleAxis(180, om) * oa;
            Vector3 b = ob + o;
            Vector3 c = mirrorPalm.position;
            Vector3 bc = c - b;
            Vector3 ad = Quaternion.AngleAxis(180, om) * bc;
            ad.y = 0;
            deviation = ad;
            thisPalm.position += deviation;
        }
        else
        {
            deviation = meshHand.deviation;
            thisPalm.localPosition = Vector3.zero;
            thisPalm.position += deviation;
        }
        Debug.DrawLine(thisPalm.position, thisPalm.position - deviation, Color.red);
    }
}
