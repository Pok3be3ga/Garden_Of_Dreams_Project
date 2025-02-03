
using UnityEngine;

public class HealtItem : Item
{
    public float HealthOfNumber;
    public override void Init(Collider2D player)
    {
        base.Init(player);
        HealthOfNumber = Random.Range(10, 20);
    }
    public override void Destroy()
    {
        base.Destroy();
        PlayerCollider.GetComponent<HealthSystem>().TakeHealth(HealthOfNumber);
    }
}
