using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed; //Player Speed

    float hAxis; //Horizontal Axis
    float vAxis; //Vertical Axis
    bool walkDown;

    Vector3 moveVector;

    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        //�ڽ� ������Ʈ�� Animator�� ��� �ֱ� ������ �ڽ� ������Ʈ���� GetComponent�� ��
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input Axis
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        //Project Settings���� Left shift�� ������ �� �ȵ��� �߰� Function �߰�
        //Left Shit�� �̸� = Walk
        walkDown = Input.GetButton("Walk"); //Shift�� �� ������ ���� �Էµ� �޾ƾ� ��

        moveVector = new Vector3(hAxis, 0, vAxis).normalized;

        //transform �̵��� ��� update, �浹 ������ ��� Fixed Update
        //�浹�� �����ϴ� ��찡 �߻�
        //Player.Rigidbody => ������ continuos�� ����

        //�����ӿ� ���߱� ���� Time.deltaTime ���
        transform.position += moveVector * speed * Time.deltaTime;

        animator.SetBool("isRun", moveVector != Vector3.zero);
        animator.SetBool("isWalk", walkDown);

        //�ٶ󺸴� �������� ȸ����
        transform.LookAt(transform.position + moveVector);
    }
   
}
