using UnityEngine;

public class VirtualJoystick : MonoBehaviour
{
    // Ссылка на движущуюся часть джойстика (stick)
    public RectTransform stick;
    // Радиус области действия джойстика (в пикселях)
    public float handleRadius = 50f;

    // Активная зона экрана (например, левая половина)
    public Rect activeZone = new Rect(0, 0, Screen.width / 2, Screen.height);

    private Vector2 inputVector;

    void Update()
    {
        // Проверяем, нажата ли кнопка мыши или тачскрин
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Берём первый тач

            // Преобразуем позицию тача в локальные координаты Canvas
            if (IsInActiveZone(touch.position))
            {
                Vector2 localTouchPosition = GetLocalPosition(touch.position);
                HandleInput(localTouchPosition);
            }
        }
        else if (Input.GetMouseButton(0)) // Для тестирования на ПК
        {
            if (IsInActiveZone(Input.mousePosition))
            {
                Vector2 localMousePosition = GetLocalPosition(Input.mousePosition);
                HandleInput(localMousePosition);
            }
        }
        else
        {
            // Если нет активных тачей или нажатий мыши, сбрасываем джойстик
            ResetJoystick();
        }
    }

    private bool IsInActiveZone(Vector2 screenPosition)
    {
        // Проверяем, находится ли позиция внутри активной зоны
        return activeZone.Contains(screenPosition);
    }

    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        // Получаем RectTransform джойстика
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Преобразуем позицию экрана в локальные координаты Canvas
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
        // Вычисляем направление движения стика относительно центра джойстика
        Vector2 direction = inputPosition - (Vector2)transform.localPosition;

        // Ограничиваем движение стика радиусом handleRadius
        direction = Vector2.ClampMagnitude(direction, handleRadius);

        // Устанавливаем позицию стика
        stick.localPosition = direction;

        // Нормализуем вектор для использования в движении персонажа
        inputVector = direction.normalized; // Используем normalized для получения единичного вектора
    }

    private void ResetJoystick()
    {
        // Сбрасываем позицию стика и вектор движения
        stick.localPosition = Vector2.zero;
        inputVector = Vector2.zero;
    }

    public Vector2 GetInputVector()
    {
        return inputVector;
    }
}