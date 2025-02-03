using UnityEngine;

public class BulletsItem : Item
{
    public int BulletOfNumber;
    public override void Init(Collider2D player)
    {
        base.Init(player);
        BulletOfNumber = Random.Range(10, 20);
    }
    public override void Destroy()
    {
        base.Destroy();
        InventorySystem.GetBullet(BulletOfNumber);
    }
}
