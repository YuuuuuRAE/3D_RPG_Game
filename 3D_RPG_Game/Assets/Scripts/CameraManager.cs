using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //Player
    public Transform Target;
    //��ġ ���� ��
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        //����ؼ� �÷��̾��� ��ġ�� �������� ���� ����ٴϵ��� ����
        transform.position = Target.position + offset;
    }
}
