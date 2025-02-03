using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    // ������� ��������
    public float currentHealth = 100f;

    // ������������ ��������
    public float maxHealth = 100f;

    // ������ �� UI-��������� HealthBar
    public Slider healthBar;

    // ����������� HealthBar ��� ��������
    public Transform healthBarTransform;

    void Start()
    {
        // ������������� ��������� �������� HealthBar
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    // ����� ��� ��������� ��������
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // ��������� HealthBar
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        // ���������, ��� �� ������
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ����� ��� ������ �������
    private void Die()
    {
        Debug.Log($"{name} �����!");
        Destroy(gameObject);
    }

    // ���������� ������� HealthBar
    //void LateUpdate()
    //{
    //    if (healthBarTransform != null)
    //    {
    //        healthBarTransform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0));
    //    }
    //}
}