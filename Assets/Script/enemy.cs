using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int Health = 100; // public 변수 "Health" 를 int 데이터타입으로 선언 후 100로 정함 (int == 정수 == 자연수)
    public float Timer = 1.0f; // public 변수 "Timer" 를 float 데이터타입으로 선언 후 1.0f로 정함 (f == float == 소수)
    public int AttackPoint = 50; // public 변수 "AttackPoint" 를 int 데이터타입으로 선언 후 50으로 정함

    // 최초 프레임 업데이트 전에 한 번 실행
    void Start()
    {
        Health = 100; // Health를 100 으로 지정한다.
    }

    // 게임 진행 중 매 프레임마다 실행
    void Update()
    {
        CharacterHealthUp();
        if (Input.GetKeyDown(KeyCode.Space))// 스페이스바를 눌렀는가?
        {
            Health -= AttackPoint; // Health를 원래 Health에서 AttackPoint의 값만큼 감소시킨다.
        }
        CheckDeath();
    }

    public void CharacterHit(int Damage) //Damage 정수 변수를 받는 함수 선언
    {
        Health -= Damage; //받은 Damage 값을 Health에서 감소
    }

    void CheckDeath() //체력 검사 함수
    {
        if (Health <= 0) // Health (체력) 변수가 값이 0과 같거나 적을 때
        {
            Destroy(gameObject); // 이 명령을 실행하는 개체 파괴
        }
    }
    void CharacterHealthUp() // 매 1초마다 체력(Health 변수) 10 증가
    {
        Timer -= Time.deltaTime; //시간을 매 프레임마다 감소 (Time.deltaTime == 1 프레임이 지난 실제 시간)

        if (Timer <= 0) // Timer 변수가 0과 같거나 작다면?
        {
            Timer = 1; // Timer 변수를 1로 지정 (set)
            Health += 10; // Health를 원래 Health에서 10 증가시킨다.
        }
    }
}
