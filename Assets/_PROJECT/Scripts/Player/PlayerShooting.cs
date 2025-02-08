using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject _weapon;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private InventorySystem _inventorySystem;
    [SerializeField] private Button _shootButton;
    public Bullet bulletPrefab;
    public Transform firePoint;

    public float fireRate = 0.5f;
    public float detectionRadius = 10f;
    public int bulletDamage = 20;

    private float _fireTimer;
    private float _findTimer;
    private List<EnemyAI> _enemies = new List<EnemyAI>();
    private EnemyAI _currentEnemy;

    void Start()
    {
        _shootButton.onClick.AddListener(Shoot);
    }

    void Update()
    {
        _fireTimer += Time.deltaTime;
        _findTimer += Time.deltaTime;
        FindClosestEnemy();
    }
    public void AddEnemy(EnemyAI enemy)
    {
        _enemies.Add(enemy);
    }
    public void RemoveEnemy(EnemyAI enemy)
    {
        _enemies.Remove(enemy);
    }
    // Метод для поиска ближайшего врага
    private void FindClosestEnemy()
    {
        if(_findTimer > 3 && _enemies.Count > 0)
        {
            float[] distances = new float[_enemies.Count];
            for (int i = 0; i < _enemies.Count; i++)
            {
                distances[i] = Vector3.Distance(transform.position, _enemies[i].transform.position);
            }
            _currentEnemy = _enemies[Array.IndexOf(distances, distances.Min())];
            _findTimer = 0;
        }
    }

    // Метод для поворота в сторону цели
    private void WeaponTarget(Vector3 targetPosition)
    {
        // Вычисляем направление к цели
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Поворачиваем игрока
        _weapon.transform.rotation = Quaternion.Euler(0, 0, angle - 90); // -90 для корректировки направления
    }

    // Метод для выполнения выстрела
    private void Shoot()
    {
        if (_fireTimer >= fireRate && _inventorySystem.CurrentBullet() > 0 && _currentEnemy != null)
        {
            // Создаём пулю в точке спавна
            Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.Init(_currentEnemy.transform);

            Vector2 direction = (_currentEnemy.transform.position - firePoint.position).normalized;
            _inventorySystem.SetBullet();
            _fireTimer = 0f;
        }
    }
}