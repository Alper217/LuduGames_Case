using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Image[] inventorySlots;
    public List<ItemData> items = new List<ItemData>();
    public static InventoryManager Instance { get; private set; }

    // Bu property'ler Chest.cs ve Door.cs tarafından çağrılır.
    // Listeyi tarayıp "Çantamda bu tipte bir eşya var mı?" diye bakar.
    public bool HasChestKey => HasItemType(ItemType.ChestKey);
    public bool HasDoorKey => HasItemType(ItemType.DoorKey);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            items.Clear();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        UpdateInventoryUI();
    }
    public bool AddItem(ItemData item)
    {
        if (items.Count >= inventorySlots.Length)
        {
            Debug.Log("Inventory Full!");
            return false;
        }

        items.Add(item);
        UpdateInventoryUI();
        return true;
    }
    
    public void RemoveItem(ItemData item)
    {
        items.Remove(item);
        UpdateInventoryUI();
    }

    // Yardımcı Fonksiyon: Listede belirli bir tipte eşya var mı?
    private bool HasItemType(ItemType type)
    {
        foreach(var item in items)
        {
            if(item.itemType == type) return true;
        }
        return false;
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < items.Count)
            {
                inventorySlots[i].sprite = items[i].itemIcon;
                inventorySlots[i].enabled = true;
            }
            else
            {
                inventorySlots[i].enabled = false;
            }
        }
    }
}
