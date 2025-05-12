using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour // ���� ������Ʈ�� ����
{

    //���� Ÿ�� ���� (0:��� , 1:��纣��, 2: ���ڳ� ....) int�� ����
    public int fruitType;

    //������ �̹� �������°�? �� ���� �÷���
    public bool hasMerged = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�̹� ������ ������ ����
        if (hasMerged)
        {
            return;
        }

        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();

        if (otherFruit != null && !otherFruit.hasMerged && otherFruit.fruitType == fruitType)
        {
            hasMerged = true;
            otherFruit.hasMerged = true;

            Vector3 mergePosition = (transform.position + otherFruit.transform.position) / 2f;

            // ���� ���Ϸ� ���׷��̵�
            FruitGame gameManager=FindObjectOfType<FruitGame>();
            if (gameManager != null)
            {
                gameManager.MergeFruits(fruitType, mergePosition);
            }

            // �浹�� 2���� ���� ����
            Destroy(otherFruit.gameObject);
            Destroy(gameObject);
        }
    }
}
