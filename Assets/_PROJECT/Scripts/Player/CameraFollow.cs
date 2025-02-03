using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Ссылка на объект персонажа
    public Transform target;

    // Скорость сглаживания движения камеры
    public float smoothSpeed = 0.125f;

    // Ограничения по оси X и Y (опционально)
    public Vector2 minXmaxX; // Минимальное и максимальное значение X
    public Vector2 minYmaxY; // Минимальное и максимальное значение Y

    void LateUpdate()
    {
        if (target != null)
        {
            // Вычисляем желаемую позицию камеры
            Vector3 desiredPosition = new Vector3(
                Mathf.Clamp(target.position.x, minXmaxX.x, minXmaxX.y),
                Mathf.Clamp(target.position.y, minYmaxY.x, minYmaxY.y),
                transform.position.z
            );

            // Плавно перемещаем камеру к желаемой позиции
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}