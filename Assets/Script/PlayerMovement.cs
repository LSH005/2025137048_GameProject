using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;  //�̼� ����
    public Rigidbody rb;    //�÷��̾� ��ü ����
    public float jumpForce = 5.0f;  //���� ÷ �� �ֱ�
    public bool isGround = true;    //���� �ִ��� ����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(moveHorizontal * moveSpeed, rb.velocity.y, moveVertical * moveSpeed);

        if (Input.GetButtonDown("Jump") && isGround)
        {
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse); //�������� ������ ����ŭ ��ü�� �� ����
            isGround = false; //������ ���������� False�� ��ȯ
        }
    }
    private void OnCollisionEnter(Collision collision) //�浹�� �� �۵��ϴ� �Լ�
    {
        if (collision.gameObject.tag == "Ground")
        {    //�浹�� �Ͼ ��ü�� �±װ� "Ground" �ΰ� ?
            isGround = true;   //���� �ε����� �ٽ� ���� ���� true
        }
    }
}
