using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;  //이속 변수
    public Rigidbody rb;    //플레이어 강체 선언
    public float jumpForce = 5.0f;  //점프 첨 값 주기
    public bool isGround = true;    //땅에 있는지 감지
    public int coinCount = 0; // 코인 갯수
    public int totalCoins = 5; //요구하는 코인 갯수



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
}
