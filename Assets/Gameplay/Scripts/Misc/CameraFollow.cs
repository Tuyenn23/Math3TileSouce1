using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform Target;
    public float Speed;


    Vector3 _temp_dis;
    private void Awake()
    {
        _temp_dis = Target.position - transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position - _temp_dis, Speed);
    }
}
