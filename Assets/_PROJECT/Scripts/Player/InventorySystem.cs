using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    // ������ �� ����� ���������
    public GameObject inventoryGrid;

    // ������ ��� �������� ���������

    private Item[] items = new Item[3];
    [SerializeField] private Button[] itemButtons;
    [SerializeField] private Button[] deleteItemButtons;
    [SerializeField] private Sprite defalthSprite;
    [SerializeField] private int _numberOfBullet;
    [SerializeField] private TextMeshProUGUI _numberOfBulletText;

    public int CurrentBullet()
    {
        return _numberOfBullet;
    }
    public void SetBullet()
    {
        _numberOfBullet--;
        _numberOfBulletText.text = _numberOfBullet.ToString();
    }
    public void GetBullet(int bullet)
    {
        _numberOfBullet += bullet;
        _numberOfBulletText.text = _numberOfBullet.ToString();
    }
    // ������� �� ���� ���������
    private bool isInventoryOpen = false;
    private void Start()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            int idx = i;
            itemButtons[i].onClick.AddListener(() => { deleteItemButtons[idx].gameObject.SetActive(!deleteItemButtons[idx].gameObject.active); });
            itemButtons[i].interactable = false;
            deleteItemButtons[i].onClick.AddListener(() => { RemoveItem(idx); deleteItemButtons[idx].gameObject.SetActive(false); });
        }
    }
    // ���������� �������� � ���������
    public void AddItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null) // ���� ������ ��������� ������
            {
                items[i] = item;
                UpdateInventoryUI();
                item.gameObject.SetActive(false);
                return;
            }
        }

        Debug.LogWarning("��������� �����!");
    }

    // ���������� UI ���������
    private void UpdateInventoryUI()
    {
        Transform[] slots = new Transform[items.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = itemButtons[i].transform;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (i <= items.Length && items[i] != null)
            {
                slots[i].GetComponent<Image>().sprite = items[i].ItemSprite;
                slots[i].GetComponent<Button>().interactable = true; // ���������� ������
            }
            else
            {
                slots[i].GetComponent<Image>().sprite = defalthSprite;
                slots[i].GetComponent<Button>().interactable = false; // ������������ ������
            }
        }
    }

    // �������� �������� �� ���������
    public void RemoveItem(int index)
    {
        if (index >= 0 && index < items.Length)
        {
            items[index].Destroy();
            items[index] = null;
            UpdateInventoryUI();
        }
    }

    // ������������ ��������� ���������
    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryGrid.SetActive(isInventoryOpen);
    }
}