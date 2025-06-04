using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float rotationSpeed = 7.5f; // ī�޶� ȸ�� �ӵ� (Time.deltaTime �� �������� ����)
    public float rotationThreshold = 0.25f; // ���� ���� Ȯ�� ����

    // �̽����� �׽�Ʈ�غ� ���, Lerp ������ 7.5, Ȯ�� ������ 0.25 ������ ������ �ӵ��� ����
    // ���� �������� �ʰ�, �ʹ� �������� ������ ��.

    private Quaternion targetRotation; // ��ǥ ȸ�� ��
    private bool isRotating = false; // ���� ȸ�� ������

    void Awake()
    {
        /*
        ���� �� ���� ī�޶��� ȸ�� ���� ��ǥ ȸ�� ������ ����.
        ī�޶� � ������ ���� �ֵ�
        �� ���¿������� ȸ�� ����.
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
        �� if�� �ΰ��� �׽�Ʈ �ڵ���.
        W ������ ���� 90�� ȸ��
        S ������ �Ʒ��� 90�� ȸ��
        */

        if (isRotating)
        {
            // Lerp�� ����Ͽ� �ε巴�� ����
            // rotationSpeed�� �������� ������ ȸ��.
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            //Quaternion = Vecter3�� ��ġ, Quaternion�� ���� (��ü�� ȸ����)

            // ��ǥ �������� ���̰� rotationThreshold �̳��̸� ��Ȯ�� ������ �����ϰ� ȸ�� ����
            if (Quaternion.Angle(transform.rotation, targetRotation) < rotationThreshold)
            {
                transform.rotation = targetRotation; // ��Ȯ�� ��ǥ ������ ����
                isRotating = false; // ȸ�� ����
                Debug.Log("�۵� �Ϸ�");
            }
        }
    }
    public void RotateCameraUp() // 90�� ����
    {
        if (isRotating) return; // �̹� ȸ�� ���̶�� ��� ��ȯ

        // ���� ȸ������ X������ -90�� (����) ȸ���ϴ� Quaternion�� ���.
        // Quaternion.Euler�� ���Ϸ� ������ Quaternion���� ��ȯ. (���� ����Ƽ�� �˾Ƶ赵�� ��ȯ�Ѵٰ� �����)
        targetRotation = Quaternion.Euler(transform.eulerAngles.x - 90f, transform.eulerAngles.y, transform.eulerAngles.z);
        isRotating = true; // ȸ�� ����
    }

    public void RotateCameraDown() // 90�� �Ʒ���
    {
        if (isRotating) return; // �̹� ȸ�� ���̶�� ��� ��ȯ

        // ���� ȸ������ X������ +90�� (�Ʒ���) ȸ���ϴ� Quaternion�� ���.
        targetRotation = Quaternion.Euler(transform.eulerAngles.x + 90f, transform.eulerAngles.y, transform.eulerAngles.z);
        isRotating = true; // ȸ�� ����
    }
}