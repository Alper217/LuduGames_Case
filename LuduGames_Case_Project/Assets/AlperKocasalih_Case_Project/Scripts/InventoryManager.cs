using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Image[] inventorySlots;
    public List<ItemData> items = new List<ItemData>();
    public static InventoryManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
         Instance = this;
      }
      else
      {
         Destroy(gameObject);
      }
   }
   public void AddItem(ItemData item)
   {
      items.Add(item);
      UpdateInventoryUI();
   }
   public void RemoveItem(ItemData item)
   {
      items.Remove(item);
      UpdateInventoryUI();
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
