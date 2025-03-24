using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float Timer = 1.0f; // public ���� "Timer" �� float ������Ÿ������ ���� �� 1.0f�� ���� (f == float == �Ҽ�)
    public GameObject EnemyObject;

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime; //�ð��� �� �����Ӹ��� ���� (Time.deltaTime == 1 �������� ���� ���� �ð�)

        if (Timer <= 0) // Timer ������ 0�� ���ų� �۴ٸ�?
        {
            Timer = 1; // Timer ������ 1�� ���� (set)
            GameObject Temp = Instantiate(EnemyObject); // ���� ��ü�� ���� �����Ѵ�
            Temp.transform.position = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0); // ��ġ ���� �̵�
        }

        if (Input.GetMouseButtonDown(0))    //���콺 ��ư�� ������ ��
        {
            RaycastHit hit; //���� hit ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ī�޶󿡼� ray�� �߻��Ͽ� 3D �������� ��ü Ȯ��

            if (Physics.Raycast(ray, out hit)) // ray���� hit�Ǵ� ��ü�� �ִ°�?
            {
                if (hit.collider != null) // ��ü�� �����ϴ°�?
                {
                    //Debug.Log($"hit:{hit.collider.name}"); // �ش� ��ü�� �̸��� Debug.Log�� ���
                    hit.collider.gameObject.GetComponent<enemy>().CharacterHit(30); //enemy ��ũ��Ʈ�� CharacterHit �Լ��� ���⼭ ȣ��
                }
            }
        }
    }
}
