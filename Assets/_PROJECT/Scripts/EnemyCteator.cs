using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCteator : MonoBehaviour
{
    [SerializeField] private EnemyAI _enemy;
    [SerializeField] private PlayerMove _player;
    [SerializeField] private PlayerShooting _playerShooting;
    [SerializeField] private float _min;
    [SerializeField] private float _max;
    void Start()
    {

        for (int i = 0; i < 3; i++)
        {
            float randomX = Random.Range(_min, _max);
            float randomZ = Random.Range(_min, _max);


            EnemyAI enemy = Instantiate(_enemy, new Vector3(randomX, 0f, randomZ), Quaternion.identity);
            _enemy.Init(_player);
            _playerShooting.AddEnemy(enemy);
        }
    }
}
