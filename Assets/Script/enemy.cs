using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int Health = 100; // public ���� "Health" �� int ������Ÿ������ ���� �� 100�� ���� (int == ���� == �ڿ���)
    public float Timer = 1.0f; // public ���� "Timer" �� float ������Ÿ������ ���� �� 1.0f�� ���� (f == float == �Ҽ�)
    public int AttackPoint = 50; // public ���� "AttackPoint" �� int ������Ÿ������ ���� �� 50���� ����

    // ���� ������ ������Ʈ ���� �� �� ����
    void Start()
    {
        Health = 100; // Health�� 100 ���� �����Ѵ�.
    }

    // ���� ���� �� �� �����Ӹ��� ����
    void Update()
    {
        CharacterHealthUp();
        if (Input.GetKeyDown(KeyCode.Space))// �����̽��ٸ� �����°�?
        {
            Health -= AttackPoint; // Health�� ���� Health���� AttackPoint�� ����ŭ ���ҽ�Ų��.
        }
        CheckDeath();
    }

    public void CharacterHit(int Damage) //Damage ���� ������ �޴� �Լ� ����
    {
        Health -= Damage; //���� Damage ���� Health���� ����
    }

    void CheckDeath() //ü�� �˻� �Լ�
    {
        if (Health <= 0) // Health (ü��) ������ ���� 0�� ���ų� ���� ��
        {
            Destroy(gameObject); // �� ����� �����ϴ� ��ü �ı�
        }
    }
    void CharacterHealthUp() // �� 1�ʸ��� ü��(Health ����) 10 ����
    {
        Timer -= Time.deltaTime; //�ð��� �� �����Ӹ��� ���� (Time.deltaTime == 1 �������� ���� ���� �ð�)

        if (Timer <= 0) // Timer ������ 0�� ���ų� �۴ٸ�?
        {
            Timer = 1; // Timer ������ 1�� ���� (set)
            Health += 10; // Health�� ���� Health���� 10 ������Ų��.
        }
    }
}
