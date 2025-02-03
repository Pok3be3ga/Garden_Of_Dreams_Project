using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite itemSprite;
    public float detectionRadius = 2f;
    public InventorySystem inventorySystem;
    private Collider2D playerCollider;

    void Start()
    {
        playerCollider = FindObjectOfType<PlayerMove>().GetComponent<Collider2D>();
    }

    void Update()
    {
        // Проверяем расстояние до персонажа
        if (playerCollider != null && Vector2.Distance(transform.position, playerCollider.transform.position) <= detectionRadius)
        {
            PickUpItem();
        }
    }

    // Подбор предмета
    private void PickUpItem()
    {
        if (inventorySystem != null)
        {
            inventorySystem.AddItem(this);
        }
        else
        {
            Debug.LogError("Инвентарь не найден!");
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}