using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    // ������� ��������
    public float CurrentHealth = 100f;
    // ������������ ��������
    public float MaxHealth = 100f;
    // ������ �� UI-��������� HealthBar
    public Slider HealthBar;
    void Start()
    {
        // ������������� ��������� �������� HealthBar
        if (HealthBar != null)
        {
            HealthBar.maxValue = MaxHealth;
            HealthBar.value = CurrentHealth;
        }
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        // ��������� HealthBar
        if (HealthBar != null)
        {
            HealthBar.value = CurrentHealth;
        }

        // ���������, ��� �� ������
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    public void TakeHealth(float health)
    {
        CurrentHealth += health;
        if (CurrentHealth >= MaxHealth) CurrentHealth = MaxHealth;
    }
    public virtual void Die()
    {
        SceneManager.LoadScene("Game");
    }

}