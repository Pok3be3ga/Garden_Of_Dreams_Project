using UnityEngine;

public class HealthSystemEnemy : HealthSystem
{
    public Item[] _items;

    public override void Die()
    {
        int random = Random.Range(0, _items.Length);
        Item item = Instantiate(_items[random], transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.parent = null;
        item.Init(gameObject.GetComponent<EnemyAI>().Player.Collider2D);
        GetComponent<EnemyAI>().RemoveEnemy();
        Destroy(gameObject);
    }
}
