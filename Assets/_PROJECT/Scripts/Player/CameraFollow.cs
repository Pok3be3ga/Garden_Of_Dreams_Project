using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // ������ �� ������ ���������
    public Transform target;

    // �������� ����������� �������� ������
    public float smoothSpeed = 0.125f;

    // ����������� �� ��� X � Y (�����������)
    public Vector2 minXmaxX; // ����������� � ������������ �������� X
    public Vector2 minYmaxY; // ����������� � ������������ �������� Y

    void LateUpdate()
    {
        if (target != null)
        {
            // ��������� �������� ������� ������
            Vector3 desiredPosition = new Vector3(
                Mathf.Clamp(target.position.x, minXmaxX.x, minXmaxX.y),
                Mathf.Clamp(target.position.y, minYmaxY.x, minYmaxY.y),
                transform.position.z
            );

            // ������ ���������� ������ � �������� �������
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}