using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    // Текущее здоровье
    public float CurrentHealth = 100f;
    // Максимальное здоровье
    public float MaxHealth = 100f;
    // Ссылка на UI-компонент HealthBar
    public Slider HealthBar;
    void Start()
    {
        // Устанавливаем начальное значение HealthBar
        if (HealthBar != null)
        {
            HealthBar.maxValue = MaxHealth;
            HealthBar.value = CurrentHealth;
        }
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        // Обновляем HealthBar
        if (HealthBar != null)
        {
            HealthBar.value = CurrentHealth;
        }

        // Проверяем, жив ли объект
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