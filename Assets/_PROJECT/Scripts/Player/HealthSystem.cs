using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    // Текущее здоровье
    public float currentHealth = 100f;

    // Максимальное здоровье
    public float maxHealth = 100f;

    // Ссылка на UI-компонент HealthBar
    public Slider healthBar;

    // Отображение HealthBar над объектом
    public Transform healthBarTransform;

    void Start()
    {
        // Устанавливаем начальное значение HealthBar
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    // Метод для изменения здоровья
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Обновляем HealthBar
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        // Проверяем, жив ли объект
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Метод для смерти объекта
    private void Die()
    {
        Debug.Log($"{name} погиб!");
        Destroy(gameObject);
    }

    // Обновление позиции HealthBar
    //void LateUpdate()
    //{
    //    if (healthBarTransform != null)
    //    {
    //        healthBarTransform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0));
    //    }
    //}
}