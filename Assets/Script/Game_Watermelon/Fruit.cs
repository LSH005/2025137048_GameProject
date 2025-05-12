using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour // 과일 오브젝트에 부착
{

    //과일 타입 결정 (0:사과 , 1:블루베리, 2: 코코넛 ....) int로 제작
    public int fruitType;

    //과일이 이미 합쳐졌는가? 에 대한 플레그
    public bool hasMerged = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //이미 합쳐진 과일은 무시
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

            // 다음 과일로 업그레이드
            FruitGame gameManager=FindObjectOfType<FruitGame>();
            if (gameManager != null)
            {
                gameManager.MergeFruits(fruitType, mergePosition);
            }

            // 충돌한 2개의 과일 제거
            Destroy(otherFruit.gameObject);
            Destroy(gameObject);
        }
    }
}
