using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed; //Player Speed
    public float jumpPower;
    public float waitDodge;

    float hAxis; //Horizontal Axis
    float vAxis; //Vertical Axis
    bool walkDown; //Check Walk Down
    bool jumpDown; //Check Jump Down

    bool isJump; //Check Jump
    bool isDodge; //Check Dodge

    Vector3 moveVector;
    Vector3 dodgeVector;
    Animator animator;
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Awake()
    {
        //자식 오브젝트에 Animator가 들어 있기 때문에 자식 오브젝트에서 GetComponent를 함
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
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
        jumpDown = Input.GetButton("Jump");

        moveVector = new Vector3(hAxis, 0, vAxis).normalized;
        //닷지 중이라면 벡터를 변경 (방향 변환을 막음)
        if (isDodge) moveVector = dodgeVector;

        //transform 이동의 경우 update, 충돌 감지의 경우 Fixed Update
        //충돌을 무시하는 경우가 발생
        //Player.Rigidbody => 감지를 continuos로 변경

        //Move
        //프레임에 맞추기 위해 Time.deltaTime 사용
        transform.position += moveVector * speed * Time.deltaTime;


        //Animation
        animator.SetBool("isRun", moveVector != Vector3.zero);
        animator.SetBool("isWalk", walkDown);

        //바라보는 방향으로 회전함
        transform.LookAt(transform.position + moveVector);

        //Jump
        if (jumpDown && !isJump && moveVector == Vector3.zero && !isDodge)
        {
            isJump = true;
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            animator.SetBool("isJump", true);
            animator.SetTrigger("doJump");
            isJump = true;
        }

        //Dodge
        if (jumpDown && !isJump && moveVector != Vector3.zero && !isDodge)
        {
            dodgeVector = moveVector;
            speed *= 2;
            animator.SetTrigger("doDodge");
            isDodge = true;
            Invoke("DodgeOut",waitDodge);
        }
    }
    
    //닷지쿨을 위한 함수
    void DodgeOut()
    {
        isDodge = false;
        speed *= 0.5f;
    }


    //연속 점프를 막기위한 Collision 체크
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            animator.SetBool("isJump", false);
            isJump = false;
        }
    }

}
