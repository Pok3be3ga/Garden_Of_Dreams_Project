using UnityEngine;

public class HealthSystemEnemy : HealthSystem
{
    public Item[] _items;
    public override void Die()
    {
        Destroy(gameObject);
        int random = Random.Range(0, _items.Length - 1);
        Item item = Instantiate(_items[random], transform);
        item.Init(gameObject.GetComponent<EnemyAI>().Player.Collider2D);
    }
}
