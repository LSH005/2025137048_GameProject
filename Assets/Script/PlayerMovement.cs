using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("�̵� ����")]
    public float moveSpeed = 5.0f;  //�̼� ����
    public Rigidbody rb;    //�÷��̾� ��ü ����
    public float jumpForce = 5.0f;  //���� ÷ �� �ֱ�
    public float TurnSpeed = 10f; // ȸ�� �ӵ� �ֱ�

    [Header("���� ���� ����")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;

    [Header("���� ���� ����")]
    public float coyoteTime = 0.15f;
    public float coyoteTimeCounter;
    public bool realGround = true;

    [Header("�۶��̴� ����")]
    public GameObject gliderObject;
    public float gliderFallSpeed = 1.0f;
    public float gliderMoveSpeed = 7.0f;
    public float gliderMaxTime = 5.0f;
    public float gliderTimeLeft;
    public bool isGliding=false;

    public bool isGround = true;    //���� �ִ��� ����
    public int coinCount = 0; // ���� ����
    public int totalCoins = 5; //�䱸�ϴ� ���� ����


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
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime; // �ϰ� �� �߷� ��ȭ
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && isGround)
        {
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse); //�������� ������ ����ŭ ��ü�� �� ����
            isGround = false; //������ ���������� False�� ��ȯ
            realGround = false;
            coyoteTimeCounter = 0;
        }
    }
    private void OnCollisionEnter(Collision collision) //�浹�� �� �۵��ϴ� �Լ�
    {
        if (collision.gameObject.tag == "Ground")
        {    //�浹�� �Ͼ ��ü�� �±װ� "Ground" �ΰ� ?
            realGround = true;   //���� �ε����� �ٽ� ���� ���� true
        }
    }

    private void OnCollisionStay(Collision collision) //�浹�� �� �۵��ϴ� �Լ�
    {
        if (collision.gameObject.tag == "Ground")
        {
            realGround = true;
        }
    }
    private void OnCollisionExit(Collision collision) //�浹�� �� �۵��ϴ� �Լ�
    {
        if (collision.gameObject.tag == "Ground")
        {
            realGround = false;
        }
    }


    void OnTriggerEnter(Collider other) // Ʈ���� ���� �����ΰ��� ����
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++; // ���� ���� 1 ���� (++ �� 1 ����)
            Destroy(other.gameObject); // ���� ����.
            Debug.Log($"���� ���� : {coinCount} / {totalCoins}"); //���� ���� ���� �м� ���·� ��� (���� ��ũ��)
        }
        if (other.CompareTag("Door")&& coinCount >= totalCoins){
            Debug.Log("���� Ŭ����"); // ���� ���� ���� �� Scene ��ȯ
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
