using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;  //이속 변수
    public Rigidbody rb;    //플레이어 강체 선언
    public float jumpForce = 5.0f;  //점프 첨 값 주기
    public bool isGround = true;    //땅에 있는지 감지

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
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse); //위쪽으로 설정한 힘만큼 강체에 힘 전달
            isGround = false; //땅에서 떨어짐으로 False로 전환
        }
    }
    private void OnCollisionEnter(Collision collision) //충돌할 때 작동하는 함수
    {
        if (collision.gameObject.tag == "Ground")
        {    //충돌이 일어난 물체의 태그가 "Ground" 인가 ?
            isGround = true;   //땅에 부딛히면 다시 점프 권한 true
        }
    }
}
