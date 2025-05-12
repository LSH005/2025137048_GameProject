using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5.0f;  //이속 변수
    public Rigidbody rb;    //플레이어 강체 선언
    public float jumpForce = 5.0f;  //점프 첨 값 주기
    public float TurnSpeed = 10f; // 회전 속도 주기

    [Header("점프 개선 설정")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;

    [Header("지연 감지 설정")]
    public float coyoteTime = 0.15f;
    public float coyoteTimeCounter;
    public bool realGround = true;

    [Header("글라이더 설정")]
    public GameObject gliderObject;
    public float gliderFallSpeed = 1.0f;
    public float gliderMoveSpeed = 7.0f;
    public float gliderMaxTime = 5.0f;
    public float gliderTimeLeft;
    public bool isGliding=false;

    public bool isGround = true;    //땅에 있는지 감지
    public int coinCount = 0; // 코인 갯수
    public int totalCoins = 5; //요구하는 코인 갯수


    void Start()
    {
        if (gliderObject != null)
        {
            gliderObject.SetActive(false);
        }
        gliderTimeLeft = gliderMaxTime;

        coyoteTimeCounter = 0;
    }


    void Update()
    {
        UpdateGroundedState();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        

        if (Input.GetKey(KeyCode.G) && gliderTimeLeft > 0 && !isGround)
        {
            if (!isGliding)
            {
                EnableGlider();
            }

            gliderTimeLeft-= Time.deltaTime;
            if (gliderTimeLeft <= 0)
            {
                DisableGlider();
            }
        else if (isGliding)
            {
                DisableGlider();
            }
        }

        if (isGliding)
        {
            ApplyGliderMovement(moveHorizontal, moveVertical);
        }
        else
        {
            rb.velocity = new Vector3(moveHorizontal * moveSpeed, rb.velocity.y, moveVertical * moveSpeed);
            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

            if (movement.magnitude > 0.1)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TurnSpeed * Time.deltaTime);
            }
        }

        if (isGround)
        {
            if (isGliding)
            {
                DisableGlider();
            }
            gliderTimeLeft=gliderMaxTime;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime; // 하강 시 중력 강화
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && isGround)
        {
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse); //위쪽으로 설정한 힘만큼 강체에 힘 전달
            isGround = false; //땅에서 떨어짐으로 False로 전환
            realGround = false;
            coyoteTimeCounter = 0;
        }
    }
    private void OnCollisionEnter(Collision collision) //충돌할 때 작동하는 함수
    {
        if (collision.gameObject.tag == "Ground")
        {    //충돌이 일어난 물체의 태그가 "Ground" 인가 ?
            realGround = true;   //땅에 부딛히면 다시 점프 권한 true
        }
    }

    private void OnCollisionStay(Collision collision) //충돌할 때 작동하는 함수
    {
        if (collision.gameObject.tag == "Ground")
        {
            realGround = true;
        }
    }
    private void OnCollisionExit(Collision collision) //충돌할 때 작동하는 함수
    {
        if (collision.gameObject.tag == "Ground")
        {
            realGround = false;
        }
    }


    void OnTriggerEnter(Collider other) // 트리거 영역 안쪽인가를 감지
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++; // 코인 변수 1 증가 (++ 은 1 증가)
            Destroy(other.gameObject); // 코인 삭제.
            Debug.Log($"코인 수집 : {coinCount} / {totalCoins}"); //수집 코인 갯수 분수 형태로 출력 (변수 매크로)
        }
        if (other.CompareTag("Door")&& coinCount >= totalCoins){
            Debug.Log("게임 클리어"); // 이후 종료 연출 및 Scene 전환
        }
        if (other.CompareTag("Misslie"))
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameObject.SetActive(false);
        Invoke("RestartGame", 3.0f);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateGroundedState()
    {
        if (realGround)
        {
            coyoteTimeCounter = coyoteTime;
            isGround = true;
        }
        else
        {
            if (coyoteTimeCounter > 0)
            {
                coyoteTime-=Time.deltaTime;
                isGround=true;
            }
            else
            {
                isGround=false;
            }
        }
    }

    void EnableGlider()
    {
        isGliding = true;
        //Debug.Log("WORKED 1");
        if (gliderObject != null)
        {
            gliderObject.SetActive(true);
            //Debug.Log("WORKED 2");
        }
        rb.velocity = new Vector3(rb.velocity.x, -gliderFallSpeed, rb.velocity.z);
    }

    void DisableGlider()
    {
        isGliding = false;
        if (gliderObject != null)
        {
            gliderObject.SetActive(false);
        }

        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }

    void ApplyGliderMovement(float horizontal, float vertical)
    {
        Vector3 gliderVelocity=new Vector3(horizontal*gliderMoveSpeed,-gliderFallSpeed,vertical*gliderMoveSpeed);
        rb.velocity = gliderVelocity;
    }
}
