using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite ItemSprite;
    public float DetectionRadius = 2f;
    public InventorySystem InventorySystem;
    public Collider2D PlayerCollider;
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, PlayerCollider.transform.position);
        if(distance < DetectionRadius) Destroy();
    }
    public virtual void Init(Collider2D player)
    {
        PlayerCollider = player;
        InventorySystem = player.GetComponent<InventorySystem>();
    }

    public virtual void PickUpItem()
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