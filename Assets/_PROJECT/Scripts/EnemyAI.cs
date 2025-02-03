using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject _sprite;
    // Ссылка на объект игрока
    public PlayerMove Player;

    // Скорость движения врага
    public float moveSpeed = 3f;

    // Радиус обнаружения игрока
    public float detectionRadius = 5f;

    // Расстояние, на котором враг останавливается
    public float stopDistance = 1f;

    // Урон, наносимый игроку
    public float damage = 10f;

    // Интервал между ударами
    public float attackInterval = 1f;

    // Ссылка на компонент Rigidbody2D
    private Rigidbody2D rb;

    // Направление взгляда (true = вправо, false = влево)
    private bool facingRight = true;

    // Таймер для атаки
    private float attackTimer;

    void Start()
    {
        // Получаем компонент Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Проверяем расстояние до игрока
        if (Player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, Player.transform.position);

            // Если игрок находится в радиусе обнаружения
            if (distanceToPlayer <= detectionRadius)
            {
                // Если игрок ближе, чем stopDistance, останавливаем врага
                if (distanceToPlayer <= stopDistance)
                {
                    rb.velocity = Vector2.zero;

                    // Атакуем игрока
                    AttackPlayer();
                }
                else
                {
                    // Вычисляем направление движения
                    Vector2 direction = (Vector2)(Player.transform.position - transform.position).normalized;

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

    // Метод для атаки игрока
    private void AttackPlayer()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            // Наносим урон игроку
            HealthSystem playerHealth = Player.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Сбрасываем таймер
            attackTimer = 0f;
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
            _sprite.transform.localScale = localScale;
        }
    }
}