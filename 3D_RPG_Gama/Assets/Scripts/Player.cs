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
        //자식 오브젝트에 Animator가 들어 있기 때문에 자식 오브젝트에서 GetComponent를 함
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input Axis
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        //Project Settings에서 Left shift를 눌렀을 때 걷도록 추가 Function 추가
        //Left Shit의 이름 = Walk
        walkDown = Input.GetButton("Walk"); //Shift는 꾹 눌렀을 때의 입력도 받아야 함

        moveVector = new Vector3(hAxis, 0, vAxis).normalized;

        //transform 이동의 경우 update, 충돌 감지의 경우 Fixed Update
        //충돌을 무시하는 경우가 발생
        //Player.Rigidbody => 감지를 continuos로 변경

        //프레임에 맞추기 위해 Time.deltaTime 사용
        transform.position += moveVector * speed * Time.deltaTime;

        animator.SetBool("isRun", moveVector != Vector3.zero);
        animator.SetBool("isWalk", walkDown);

        //바라보는 방향으로 회전함
        transform.LookAt(transform.position + moveVector);
    }
   
}
