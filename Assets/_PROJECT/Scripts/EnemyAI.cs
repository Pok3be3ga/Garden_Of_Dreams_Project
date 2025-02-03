using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Ссылка на объект игрока
    public Transform player;

    // Скорость движения врага
    public float moveSpeed = 3f;

    // Радиус обнаружения игрока
    public float detectionRadius = 5f;

    // Расстояние, на котором враг останавливается
    public float stopDistance = 1f;

    // Ссылка на компонент Rigidbody2D
    private Rigidbody2D rb;

    // Направление взгляда (true = вправо, false = влево)
    private bool facingRight = true;

    void Start()
    {
        // Получаем компонент Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Проверяем расстояние до игрока
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Если игрок находится в радиусе обнаружения
            if (distanceToPlayer <= detectionRadius)
            {
                // Если игрок ближе, чем stopDistance, останавливаем врага
                if (distanceToPlayer <= stopDistance)
                {
                    rb.velocity = Vector2.zero;
                }
                else
                {
                    // Вычисляем направление движения
                    Vector2 direction = (Vector2)(player.position - transform.position).normalized;

                    // Двигаем врага в сторону игрока
                    rb.velocity = direction * moveSpeed;

                    // Поворачиваем врага в сторону игрока
                    Flip(direction.x);
                }
            }
            else
            {
                // Если игрок вне радиуса обнаружения, останавливаем врага
                rb.velocity = Vector2.zero;
            }
        }
    }

    // Метод для поворота врага
    private void Flip(float directionX)
    {
        if (directionX > 0 && !facingRight || directionX < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1; // Инвертируем значение по оси X
            transform.localScale = localScale;
        }
    }

    // Отображение радиусов обнаружения и остановки в редакторе Unity
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}