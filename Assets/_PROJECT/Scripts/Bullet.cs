
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 0.5f;
    private Transform _target;
    private Vector3 _direction;
    public void Init(Transform target)
    {
        _target = target;
        _direction = _target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(_direction);
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0, transform.localRotation.z);
        float distance = Vector3.Distance(transform.position, _target.position);
        Destroy(gameObject, distance / _speed);
    }
    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HealthSystemEnemy>())
        {
            _target.GetComponent<HealthSystemEnemy>().TakeDamage(20);
            Destroy(gameObject);
        }
    }
}
