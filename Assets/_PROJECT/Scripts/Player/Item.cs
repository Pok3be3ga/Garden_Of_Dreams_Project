using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite ItemSprite;
    public float DetectionRadius = 2f;
    public InventorySystem InventorySystem;
    public Collider2D PlayerCollider;
    public virtual void Init(Collider2D player)
    {
        PlayerCollider = player;
        InventorySystem = player.GetComponent<InventorySystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == PlayerCollider)
        {
            PickUpItem();
        }
    }
    private void PickUpItem()
    {
        if (InventorySystem != null)
        {
            InventorySystem.AddItem(this);
        }
    }
    public virtual void Destroy()
    {
        Destroy(gameObject);
    }
}