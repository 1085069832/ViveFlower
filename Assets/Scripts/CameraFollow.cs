using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float dis = 1f;
    Camera _camera;
    Vector3 offset;
    float yVelocity = 0;
    // Use this for initialization
    void Start()
    {
        _camera = Camera.main;
        offset = _camera.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        _camera.transform.position = transform.position + offset;
        _camera.transform.LookAt(transform);

        //float x = SmoothMove(_camera.transform.position.x, transform.position.x + transform.up.magnitude * 2f);
        //float y = SmoothMove(_camera.transform.position.y, transform.position.y + transform.up.magnitude * 2f);
        //float z = SmoothMove(_camera.transform.position.z, transform.position.z + transform.up.magnitude * 2f);
        //_camera.transform.position = new Vector3(x, _camera.transform.position.y, z);
        //_camera.transform.LookAt(transform);

    }

    public float SmoothMove(float value, float target)
    {
        return Mathf.SmoothDamp(value, target, ref yVelocity, 0.07f);
    }

#if UNITY_EDITOR
    [ContextMenu("Test1")]
    public float Test1()
    {
        return 0;
    }

    [ContextMenu("Test2")]
    void Test2()
    {
        print("test2");
    }
#endif
}
