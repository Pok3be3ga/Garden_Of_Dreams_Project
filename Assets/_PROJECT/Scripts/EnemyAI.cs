using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // ������ �� ������ ������
    public Transform player;

    // �������� �������� �����
    public float moveSpeed = 3f;

    // ������ ����������� ������
    public float detectionRadius = 5f;

    // ����������, �� ������� ���� ���������������
    public float stopDistance = 1f;

    // ������ �� ��������� Rigidbody2D
    private Rigidbody2D rb;

    // ����������� ������� (true = ������, false = �����)
    private bool facingRight = true;

    void Start()
    {
        // �������� ��������� Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ��������� ���������� �� ������
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // ���� ����� ��������� � ������� �����������
            if (distanceToPlayer <= detectionRadius)
            {
                // ���� ����� �����, ��� stopDistance, ������������� �����
                if (distanceToPlayer <= stopDistance)
                {
                    rb.velocity = Vector2.zero;
                }
                else
                {
                    // ��������� ����������� ��������
                    Vector2 direction = (Vector2)(player.position - transform.position).normalized;

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

    // ����� ��� �������� �����
    private void Flip(float directionX)
    {
        if (directionX > 0 && !facingRight || directionX < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1; // ����������� �������� �� ��� X
            transform.localScale = localScale;
        }
    }

    // ����������� �������� ����������� � ��������� � ��������� Unity
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}