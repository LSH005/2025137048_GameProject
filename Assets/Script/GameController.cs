using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float Timer = 1.0f; // public 변수 "Timer" 를 float 데이터타입으로 선언 후 1.0f로 정함 (f == float == 소수)
    public GameObject EnemyObject;

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime; //시간을 매 프레임마다 감소 (Time.deltaTime == 1 프레임이 지난 실제 시간)

        if (Timer <= 0) // Timer 변수가 0과 같거나 작다면?
        {
            Timer = 1; // Timer 변수를 1로 지정 (set)
            GameObject Temp = Instantiate(EnemyObject); // 현재 개체를 복제 생성한다
            Temp.transform.position = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0); // 위치 랜덤 이동
        }

        if (Input.GetMouseButtonDown(0))    //마우스 버튼을 눌렀을 때
        {
            RaycastHit hit; //물리 hit 선언
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //카메라에서 ray를 발사하여 3D 공간상의 물체 확인

            if (Physics.Raycast(ray, out hit)) // ray에서 hit되는 물체가 있는가?
            {
                if (hit.collider != null) // 물체가 존재하는가?
                {
                    //Debug.Log($"hit:{hit.collider.name}"); // 해당 물체의 이름을 Debug.Log에 출력
                    hit.collider.gameObject.GetComponent<enemy>().CharacterHit(30); //enemy 스크립트의 CharacterHit 함수를 여기서 호출
                }
            }
        }
    }
}
