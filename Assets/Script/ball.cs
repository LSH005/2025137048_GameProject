using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) //충돌할 때 작동하는 함수
    {
        if (collision.gameObject.tag == "Ground")
        {    //충돌이 일어난 물체의 태그가 "Ground" 인가 ?
            Debug.Log("땅과 충돌함!");   //로그 출력
        }
    }

    private void OnTriggerEnter(Collider other) // 트리거 영역 안쪽인가를 감지
    {
        Debug.Log("큐브 범위 안");
    }
}
