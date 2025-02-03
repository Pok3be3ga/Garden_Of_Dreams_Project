using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject _sprite;
    // ������ �� ������ ������
    public PlayerMove Player;

    // �������� �������� �����
    public float moveSpeed = 3f;

    // ������ ����������� ������
    public float detectionRadius = 5f;

    // ����������, �� ������� ���� ���������������
    public float stopDistance = 1f;

    // ����, ��������� ������
    public float damage = 10f;

    // �������� ����� �������
    public float attackInterval = 1f;

    // ������ �� ��������� Rigidbody2D
    private Rigidbody2D rb;

    // ����������� ������� (true = ������, false = �����)
    private bool facingRight = true;

    // ������ ��� �����
    private float attackTimer;

    void Start()
    {
        // �������� ��������� Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ��������� ���������� �� ������
        if (Player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, Player.transform.position);

            // ���� ����� ��������� � ������� �����������
            if (distanceToPlayer <= detectionRadius)
            {
                // ���� ����� �����, ��� stopDistance, ������������� �����
                if (distanceToPlayer <= stopDistance)
                {
                    rb.velocity = Vector2.zero;

                    // ������� ������
                    AttackPlayer();
                }
                else
                {
                    // ��������� ����������� ��������
                    Vector2 direction = (Vector2)(Player.transform.position - transform.position).normalized;

                    // ������� ����� � ������� ������
                    rb.velocity = direction * moveSpeed;

                    // ������������ ����� � ������� ������
                    Flip(direction.x);
                }
            }
            else
            {
                // ���� ����� ��� ������� �����������, ������������� �����
                rb.velocity = Vector2.zero;
            }
        }
    }

    // ����� ��� ����� ������
    private void AttackPlayer()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            // ������� ���� ������
            HealthSystem playerHealth = Player.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // ���������� ������
            attackTimer = 0f;
        }
    }

    // ����� ��� �������� �����
    private void Flip(float directionX)
    {
        if (directionX > 0 && !facingRight || directionX < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1; // ����������� �������� �� ��� X
            _sprite.transform.localScale = localScale;
        }
    }
}