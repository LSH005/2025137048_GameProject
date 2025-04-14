using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;  //�̼� ����
    public Rigidbody rb;    //�÷��̾� ��ü ����
    public float jumpForce = 5.0f;  //���� ÷ �� �ֱ�
    public bool isGround = true;    //���� �ִ��� ����
    public int coinCount = 0; // ���� ����
    public int totalCoins = 5; //�䱸�ϴ� ���� ����



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
}
