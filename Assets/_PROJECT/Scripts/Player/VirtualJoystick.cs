using UnityEngine;

public class VirtualJoystick : MonoBehaviour
{
    // ������ �� ���������� ����� ��������� (stick)
    public RectTransform stick;
    // ������ ������� �������� ��������� (� ��������)
    public float handleRadius = 50f;

    // �������� ���� ������ (��������, ����� ��������)
    public Rect activeZone = new Rect(0, 0, Screen.width / 2, Screen.height);

    private Vector2 inputVector;

    void Update()
    {
        // ���������, ������ �� ������ ���� ��� ��������
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // ���� ������ ���

            // ����������� ������� ���� � ��������� ���������� Canvas
            if (IsInActiveZone(touch.position))
            {
                Vector2 localTouchPosition = GetLocalPosition(touch.position);
                HandleInput(localTouchPosition);
            }
        }
        else if (Input.GetMouseButton(0)) // ��� ������������ �� ��
        {
            if (IsInActiveZone(Input.mousePosition))
            {
                Vector2 localMousePosition = GetLocalPosition(Input.mousePosition);
                HandleInput(localMousePosition);
            }
        }
        else
        {
            // ���� ��� �������� ����� ��� ������� ����, ���������� ��������
            ResetJoystick();
        }
    }

    private bool IsInActiveZone(Vector2 screenPosition)
    {
        // ���������, ��������� �� ������� ������ �������� ����
        return activeZone.Contains(screenPosition);
    }

    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        // �������� RectTransform ���������
        RectTransform rectTransform = GetComponent<RectTransform>();

        // ����������� ������� ������ � ��������� ���������� Canvas
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            screenPosition,
            null,
            out localPosition
        );

        return localPosition;
    }

    private void HandleInput(Vector2 inputPosition)
    {
        // ��������� ����������� �������� ����� ������������ ������ ���������
        Vector2 direction = inputPosition - (Vector2)transform.localPosition;

        // ������������ �������� ����� �������� handleRadius
        direction = Vector2.ClampMagnitude(direction, handleRadius);

        // ������������� ������� �����
        stick.localPosition = direction;

        // ����������� ������ ��� ������������� � �������� ���������
        inputVector = direction.normalized; // ���������� normalized ��� ��������� ���������� �������
    }

    private void ResetJoystick()
    {
        // ���������� ������� ����� � ������ ��������
        stick.localPosition = Vector2.zero;
        inputVector = Vector2.zero;
    }

    public Vector2 GetInputVector()
    {
        return inputVector;
    }
}