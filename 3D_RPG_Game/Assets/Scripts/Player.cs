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
        //�ڽ� ������Ʈ�� Animator�� ��� �ֱ� ������ �ڽ� ������Ʈ���� GetComponent�� ��
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
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
        jumpDown = Input.GetButton("Jump");

        moveVector = new Vector3(hAxis, 0, vAxis).normalized;
        //���� ���̶�� ���͸� ���� (���� ��ȯ�� ����)
        if (isDodge) moveVector = dodgeVector;

        //transform �̵��� ��� update, �浹 ������ ��� Fixed Update
        //�浹�� �����ϴ� ��찡 �߻�
        //Player.Rigidbody => ������ continuos�� ����

        //Move
        //�����ӿ� ���߱� ���� Time.deltaTime ���
        transform.position += moveVector * speed * Time.deltaTime;


        //Animation
        animator.SetBool("isRun", moveVector != Vector3.zero);
        animator.SetBool("isWalk", walkDown);

        //�ٶ󺸴� �������� ȸ����
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
    
    //�������� ���� �Լ�
    void DodgeOut()
    {
        isDodge = false;
        speed *= 0.5f;
    }


    //���� ������ �������� Collision üũ
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            animator.SetBool("isJump", false);
            isJump = false;
        }
    }

}
