using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float GameTimer = 0.0f;                          //���� Ÿ�̸Ӹ� ���� �Ѵ�.
    public GameObject MonsterGO;                            //���� ���� ������Ʈ�� ���� �Ѵ�.

    //Update is called once per frame
    void Update()
    {
        GameTimer -= Time.deltaTime;                        //�ð��� �� �����Ӹ��� ���� ��Ų��.(deltaTime �����Ӱ��� �ð� ������ �ǹ��Ѵ�.)

        if (GameTimer <= 0)                                  //���� Timer�� ��ġ�� 0���Ϸ� ������ ���
        {
            GameTimer = 3.0f;                                //�ٽ� �ʷ� ���� �����ش�.

            GameObject Temp = Instantiate(MonsterGO);
            Temp.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-4, 4), 0.0f);
            //X -10 ~ 10 Y -4 ~ 4 �� ������ �������� ��ġ ��Ų��.
        }
        //
    }                                

        
     
       
       
}
