using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    static int equipmentCounter;

    public List<ItemData> startingItems;

    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;
    public List<InventoryItem> inventory;
    public Dictionary<int,InventoryItem> inventoryDictionary;

    public List<InventoryItem> stash;
    public Dictionary<ItemData,InventoryItem> stashDictionary;


    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform statSlotParent;


    private UI_ItemSlot[] InventoryItemSlot;
    private UI_ItemSlot[] stashItemSlot;
    private UI_EquipmentSlot[] equipmentSlot;
    private UI_StatSlot[] statSlot;
    //public List<ItemData> inventory = new List<ItemData>();

    [Header("Items cooldown")]
    private float LastTimeUsedFlask;
    private float LastTimeUsedArmor;

    public float flaskCooldown {get; private set;}
    private float armorCooldown;




    private void Awake() 
    {
      if(instance == null)
        instance =this;
      else
        Destroy(gameObject);
    }

    private void Start()
    {
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<int, InventoryItem>();

        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();


        InventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        stashItemSlot = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        statSlot = statSlotParent.GetComponentsInChildren<UI_StatSlot>();

        AddStartingItems();
    }

    private void AddStartingItems()
    {
        for (int i = 0; i < startingItems.Count; i++)
        {
            if(startingItems[i] != null)
            AddItem(startingItems[i]);

        }
    }

    public void EquipItem(ItemData _item)
    {
      ItemData_Equipment newEquipment = _item as ItemData_Equipment;
      InventoryItem newItem = new InventoryItem(newEquipment);

      ItemData_Equipment oldEquipment = null;

      foreach(KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
      {
        if(item.Key.equipmentType == newEquipment.equipmentType)
          oldEquipment = item.Key;
      }

      if(oldEquipment != null)
      {
          UnequipItem(oldEquipment);
          AddItem(oldEquipment);
      }
        

      equipment.Add(newItem);
      equipmentDictionary.Add(newEquipment, newItem);

      newEquipment.AddModifiers();

      RemoveItem(_item);

      UpdateSlotUI();
    }

    public void UnequipItem(ItemData_Equipment itemToRemove)
    {
       if(equipmentDictionary.TryGetValue(itemToRemove, out InventoryItem value))
      {
        equipment.Remove(value);//장비창에서제거
        equipmentDictionary.Remove(itemToRemove);//장비딕션에서 제거
        itemToRemove.RemoveModifiers();//효과제거
      }
    }

    private void UpdateSlotUI()
    {

        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentSlot[i].slotType)
                    equipmentSlot[i].UpdateSlot(item.Value);
            }
        }


        for (int i = 0; i < InventoryItemSlot.Length; i++)
        {
            InventoryItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < stashItemSlot.Length; i++)
        {
            stashItemSlot[i].CleanUpSlot();
        }


        for (int i = 0; i < inventory.Count; i++)
        {
            InventoryItemSlot[i].UpdateSlot(inventory[i]);
        }

        for (int i = 0; i < stash.Count; i++)
        {
            stashItemSlot[i].UpdateSlot(stash[i]);
        }

        UpdateStatsUI();
    }

    public void UpdateStatsUI()
    {
        for (int i = 0; i < statSlot.Length; i++)//update info of stats in character UI
        {
            statSlot[i].UpdateStatValueUI();
        }
    }

    public void AddItem(ItemData _item)
    {
      if(_item.itemType == ItemType.Equipment && CanAddItem())
        AddToInventory(_item);

      else if (_item.itemType == ItemType.Material)
        AddToStash(_item);


      UpdateSlotUI();

    }

    private void AddToStash(ItemData _item)
    {
       if(stashDictionary.TryGetValue(_item, out InventoryItem value))
        {
          value.AddStack();
        }
        else
        {
          InventoryItem newItem = new InventoryItem(_item);
          stash.Add(newItem);
          stashDictionary.Add(_item,newItem);
        }
    }
    
     private void AddToInventory(ItemData _item)
    {
        if (_item.itemType == ItemType.Material)
        {
            if(stashDictionary.TryGetValue(_item, out InventoryItem value))
            {
               value.AddStack();
            }
            else
            {
                        InventoryItem newItem = new InventoryItem(_item);
          stash.Add(newItem);
          stashDictionary.Add(_item,newItem);
            }

        }
        else
        {
             ItemData_Equipment newEquipment = _item as ItemData_Equipment;
            int itemID = ((int)newEquipment.equipmentType * 1000) + Inventory.equipmentCounter++;
            newEquipment.ID = itemID;
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictionary.Add(newEquipment.ID, newItem);
        }
    }
/*
     private void AddToInventory(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }
    }
*/
    

    private void AddToInventoryEquip(ItemData _item)
    {

        InventoryItem newItem = new InventoryItem(_item);
        inventory.Add(newItem);
       // inventoryDictionary.Add(_item, newItem);

    }

    public void RemoveItem(ItemData _item)
    {
      if(inventoryDictionary.TryGetValue(_item.ID, out InventoryItem value))
      {
        if(value.stackSize <=1)
        {
           inventory.Remove(value);
           inventoryDictionary.Remove(_item.ID); 
        }    
        else
            value.RemoveStack();
     }


     if(stashDictionary.TryGetValue(_item, out InventoryItem stashValue))
     {
      if(stashValue.stackSize <=1)
      {
        stash.Remove(stashValue);
        stashDictionary.Remove(_item);
      }
      else
        stashValue.RemoveStack();

     }

      UpdateSlotUI();

    }

    public bool CanAddItem()
    {
      if(inventory.Count >= InventoryItemSlot.Length)
      {
        Debug.Log("No more Space");
        return false;
      }
      return true;
    } 

    public bool CanCraft(ItemData_Equipment _itemToCraft, List<InventoryItem> _requiredMaterials)
    {
         List<InventoryItem> _materialsToRemove = new List<InventoryItem>();
 
        for (int i = 0; i < _requiredMaterials.Count ; i++)
        {
            if (stashDictionary.TryGetValue(_requiredMaterials[i].data, out InventoryItem stashValue))
            {
                if (stashValue.stackSize < _requiredMaterials[i].stackSize)
                {
                    Debug.Log("Not enough Materials in stack");
                    return false;
                }
                else
                {
                    int amountToAddToList = _requiredMaterials[i].stackSize;
                    while (amountToAddToList > 0)
                    {
                        _materialsToRemove.Add(stashValue);
                        amountToAddToList--;
 
                    }
                }
            }
            else
            {
                Debug.Log("Not enough Materials");
                return false;
            }
        }


      //인벤에서삭제
      for (int i = 0; i < _materialsToRemove.Count; i++)
      {
        RemoveItem(_materialsToRemove[i].data);
      }

      //크래프트 할 아이템 추가

      AddItem(_itemToCraft);
      Debug.Log(_itemToCraft.name + "is crafted");

      return true;

    }
    
    public List<InventoryItem> GetEquipmentList() => equipment;

    public List<InventoryItem> GetStashList() => stash;

    public ItemData_Equipment GetEquipment(EquipmentType _type)
    {
      ItemData_Equipment equipedItem = null;

      foreach(KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
      {
        if(item.Key.equipmentType == _type)
          equipedItem = item.Key;
      }
      return equipedItem;
    }
    private void Update() 
    {


      if(Input.GetKeyDown(KeyCode.L))
      {
        ItemData newItem = inventory[inventory.Count -1].data;

        RemoveItem(newItem);
      }
    }


    public void UseFlask()
    {
      ItemData_Equipment currentflask = GetEquipment(EquipmentType.Flask);

      if(currentflask == null)
      return;

      bool canUseFlask = Time.time > LastTimeUsedFlask + flaskCooldown;

      if(canUseFlask)
      {
        flaskCooldown = currentflask.itemCooldown;
        currentflask.Effect(null);
        LastTimeUsedFlask = Time.time;
      }
      else
      Debug.Log("Flask on cooldown");
      //if can use //check coolodwn     
      //use
    }

    public bool CanUseArmor()
    {
      ItemData_Equipment currentArmor = GetEquipment(EquipmentType.Armor);

      if(Time.time > LastTimeUsedArmor + armorCooldown)
      {
        armorCooldown = currentArmor.itemCooldown;
        LastTimeUsedArmor = Time.time;
        return true;
      }

      Debug.Log("Armor effect is cooldown");

      return false;
    }
}
