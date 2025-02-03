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
        // ��������� ���������� �� ���������
        if (playerCollider != null && Vector2.Distance(transform.position, playerCollider.transform.position) <= detectionRadius)
        {
            PickUpItem();
        }
    }

    // ������ ��������
    private void PickUpItem()
    {
        if (inventorySystem != null)
        {
            inventorySystem.AddItem(this);
        }
        else
        {
            Debug.LogError("��������� �� ������!");
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}