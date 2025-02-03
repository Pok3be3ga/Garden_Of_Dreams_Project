using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Ссылка на компонент Rigidbody2D
    private Rigidbody2D rb;

    // Скорость движения персонажа
    public float moveSpeed = 5f;

    // Ссылка на виртуальный джойстик
    public VirtualJoystick virtualJoystick;

    // Начальное направление (true = вправо, false = влево)
    private bool facingRight = true;

    private void Start()
    {
        // Получаем компонент Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Получаем вектор движения из джойстика
        Vector2 movement = virtualJoystick.GetInputVector();

        // Применяем движение к персонажу
        rb.velocity = movement * moveSpeed;

        // Поворачиваем персонажа в сторону движения
        Flip(movement);
    }

    private void Flip(Vector2 movement)
    {
        // Проверяем горизонтальную составляющую вектора движения
        if (movement.x > 0 && !facingRight)
        {
            // Персонаж двигается вправо, но смотрит влево — меняем направление
            FlipCharacter();
        }
        else if (movement.x < 0 && facingRight)
        {
            // Персонаж двигается влево, но смотрит вправо — меняем направление
            FlipCharacter();
        }
    }

    private void FlipCharacter()
    {
        // Изменяем масштаб по оси X для зеркального отражения спрайта
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Инвертируем значение по оси X
        transform.localScale = localScale;
    }
}