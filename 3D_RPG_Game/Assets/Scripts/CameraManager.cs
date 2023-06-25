using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //Player
    public Transform Target;
    //위치 고정 값
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        //계속해서 플레이어의 위치에 고정값을 더해 따라다니도록 설정
        transform.position = Target.position + offset;
    }
}
