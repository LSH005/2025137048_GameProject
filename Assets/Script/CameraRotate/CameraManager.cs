using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float rotationSpeed = 7.5f; // 카메라 회전 속도 (Time.deltaTime 에 곱해져서 계산됨)
    public float rotationThreshold = 0.25f; // 선형 보간 확정 범위

    // 이시현이 테스트해본 결과, Lerp 보간은 7.5, 확정 범위는 0.25 정도가 적당한 속도가 나옴
    // 딱히 느리지도 않고, 너무 빠르지도 않은듯 함.

    private Quaternion targetRotation; // 목표 회전 값
    private bool isRotating = false; // 현재 회전 중인지

    void Awake()
    {
        /*
        시작 시 현재 카메라의 회전 값을 목표 회전 값으로 설정.
        카메라가 어떤 방향을 보고 있든
        그 상태에서부터 회전 시작.
        */
        targetRotation = transform.rotation;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            RotateCameraUp();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            RotateCameraDown();
        }
        /*
        위 if문 두개는 테스트 코드임.
        W 누르면 위로 90도 회전
        S 누르면 아래로 90도 회전
        */

        if (isRotating)
        {
            // Lerp를 사용하여 부드럽게 보간
            // rotationSpeed가 높을수록 빠르게 회전.
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            //Quaternion = Vecter3는 위치, Quaternion은 각도 (개체의 회전각)

            // 목표 각도와의 차이가 rotationThreshold 이내이면 정확한 각도로 설정하고 회전 중지
            if (Quaternion.Angle(transform.rotation, targetRotation) < rotationThreshold)
            {
                transform.rotation = targetRotation; // 정확한 목표 각도로 설정
                isRotating = false; // 회전 중지
                Debug.Log("작동 완료");
            }
        }
    }
    public void RotateCameraUp() // 90도 위로
    {
        if (isRotating) return; // 이미 회전 중이라면 즉시 반환

        // 현재 회전에서 X축으로 -90도 (위로) 회전하는 Quaternion을 계산.
        // Quaternion.Euler는 오일러 각도를 Quaternion으로 변환. (대충 유니티가 알아듣도록 변환한다고 보면됨)
        targetRotation = Quaternion.Euler(transform.eulerAngles.x - 90f, transform.eulerAngles.y, transform.eulerAngles.z);
        isRotating = true; // 회전 시작
    }

    public void RotateCameraDown() // 90도 아래로
    {
        if (isRotating) return; // 이미 회전 중이라면 즉시 반환

        // 현재 회전에서 X축으로 +90도 (아래로) 회전하는 Quaternion을 계산.
        targetRotation = Quaternion.Euler(transform.eulerAngles.x + 90f, transform.eulerAngles.y, transform.eulerAngles.z);
        isRotating = true; // 회전 시작
    }
}