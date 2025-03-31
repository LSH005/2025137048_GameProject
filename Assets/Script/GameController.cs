using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float GameTimer = 0.0f;                          //게임 타이머를 설정 한다.
    public GameObject MonsterGO;                            //몬스터 게임 오브젝트를 선언 한다.

    //Update is called once per frame
    void Update()
    {
        GameTimer -= Time.deltaTime;                        //시간을 매 프레임마다 감소 시킨다.(deltaTime 프레임간의 시간 간격을 의미한다.)

        if (GameTimer <= 0)                                  //만약 Timer의 수치가 0이하로 내려갈 경우
        {
            GameTimer = 3.0f;                                //다시 초로 변경 시켜준다.

            GameObject Temp = Instantiate(MonsterGO);
            Temp.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-4, 4), 0.0f);
            //X -10 ~ 10 Y -4 ~ 4 의 범위의 랜덤으로 위치 시킨다.
        }
        //
    }                                

        
     
       
       
}
